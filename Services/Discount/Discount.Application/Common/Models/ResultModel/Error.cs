namespace Discount.Application.Common.Models.ResultModel;

public class Error
{
  public string? Code { get; init; }
  public string? Message { get; init; }
  public string Type { get; init; }
  public ValidationError[]? ValidationErrors { get; init; }

  private Error(string? code, string? message, ErrorType type, ValidationError[]? validationErrors)
  {
    Code = code;
    Message = message;
    Type = type.ToString();
    ValidationErrors = validationErrors;
  }

  public static Error Create(string code, string message, ErrorType type)
  {
    return new Error(code, message, type, null);
  }

  public static Error Create(string code, string message, ErrorType type, ValidationError[] validationErrors)
  {
    return new Error(code, message, type, validationErrors);
  }
};