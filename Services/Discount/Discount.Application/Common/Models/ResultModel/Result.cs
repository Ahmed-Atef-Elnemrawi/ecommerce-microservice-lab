namespace Discount.Application.Common.Models.ResultModel;

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

  public static Result<T> Success(T data)
  {
    return new Result<T>(data, true, null);
  }

  public static Result<T> Failure(Error error)
  {
    return new Result<T>(default, false, error);
  }

  public static Result<T> Failure(string code, string message, ErrorType type)
  {
    return new Result<T>(default, false, Error.Create(code, message, type));
  }

  public static Result<T> Failure(string code, string message, ErrorType type, ValidationError[] validationErrors)
  {
    return new Result<T>(default, false, Error.Create(code, message, type, validationErrors));
  }
}