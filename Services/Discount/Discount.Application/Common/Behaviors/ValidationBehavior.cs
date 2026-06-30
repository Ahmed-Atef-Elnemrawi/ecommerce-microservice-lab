using Discount.Application.Common.Models.ResultModel;
using FluentValidation;
using MediatR;

namespace Discount.Application.Common.Behaviors;

public sealed class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
  : IPipelineBehavior<TRequest, TResponse>
  where TRequest : notnull
{
  public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
    CancellationToken cancellationToken)
  {
    if (!validators.Any()) return await next(cancellationToken);

    var validationResult = await Task.WhenAll(validators.Select(x => x.ValidateAsync(request, cancellationToken)));

    var errors = validationResult.SelectMany(x => x.Errors)
      .Where(x => x is not null)
      .GroupBy(x => x.PropertyName)
      .Select(x => new ValidationError(x.Key, x.Select(e => e.ErrorMessage).ToArray()))
      .ToArray();

    if (errors.Length == 0) return await next(cancellationToken);

    var validationError = Error.Create("Validation.Error", "one or more validation errors occurred.",
      ErrorType.Validation, errors);

    var failureMethod = typeof(Result<>).GetMethod(nameof(Result<>.Failure), [typeof(Error)]);

    if (failureMethod is null)
      throw new InvalidOperationException(
        $"{typeof(TResponse).Name} does not contain a static Failure(Error) method.");

    var result = failureMethod.Invoke(null, [validationError]);

    return (TResponse)result!;
  }
}