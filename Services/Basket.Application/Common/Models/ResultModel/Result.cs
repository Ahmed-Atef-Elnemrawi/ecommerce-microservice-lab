namespace Basket.Application.Common.Models.ResultModel;

public class Result<T>
{
  public T? Data { get; init; }
  public bool IsSuccess { get; init; }
  public Error? Error { get; init; }


  private Result(T? data, bool isSuccess, Error? error)
  {
    Data = data;
    IsSuccess = isSuccess;
    Error = error;
  }

  public static Result<T> Success(T data) => new(data, true, null);

  public static Result<T> Failure(Error error) => new(default(T), false, error);

  public static Result<T> Failure(string code, string message, ErrorType type) =>
    new Result<T>(default, false, Error.Create(code, message, type));

  public static Result<T> Failure(string code, string message, ErrorType type, ValidationError[] validationErrors) =>
    new Result<T>(default, false, Error.Create(code, message, type, validationErrors));
}