using Basket.Application.Common.Models.ResultModel;
using FluentValidation;
using MediatR;

namespace Basket.Application.Common.Behaviors;

public sealed class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
  : IPipelineBehavior<TRequest, TResponse>
  where TRequest : notnull
{
  public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
    CancellationToken cancellationToken)
  {
    if (!validators.Any())
      return await next(cancellationToken);

    var validationResults = await Task.WhenAll(
      validators.Select(v => v.ValidateAsync(request, cancellationToken)));

    var errors = validationResults
      .SelectMany(r => r.Errors)
      .Where(e => e is not null)
      .GroupBy(e => e.PropertyName)
      .Select(e => new ValidationError(e.Key, e.Select(x => x.ErrorMessage).Distinct().ToArray()))
      .ToArray();

    if (errors.Length == 0)
      return await next(cancellationToken);

    var validationError = Error.Create(
      "Validation.Errors",
      "One or more validation errors occurred.",
      ErrorType.Validation,
      errors);

    if (!typeof(TResponse).IsGenericType || typeof(TResponse).GetGenericTypeDefinition() != typeof(Result<>))
      return await next(cancellationToken);
        

    var failureMethod = typeof(TResponse).GetMethod(nameof(Result<>.Failure), [typeof(Error)]);

    if (failureMethod is null)
      throw new InvalidOperationException(
        $"{typeof(TResponse).Name} does not contain a static Failure(Error) method.");

    var result = failureMethod.Invoke(null, [validationError]);

    return (TResponse)result!;
  }
}