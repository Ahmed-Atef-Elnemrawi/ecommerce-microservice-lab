namespace Catalog.Application.Common.Models;

public class Error
{
  public string? Code { get; init; }
  public string? Message { get; init; }
  public ErrorType Type { get; init; }
  public ValidationError[]? ValidationErrors { get; init; }

  private Error(string? code, string? message, ErrorType type, ValidationError[]? validationErrors)
  {
    Code = code;
    Message = message;
    Type = type;
    ValidationErrors = validationErrors;
  }

  public static Error Create(string code, string message, ErrorType type)
    => new(code, message, type, null);

  public static Error Create(string code, string message, ErrorType type, ValidationError[] validationErrors)
    => new(code, message, type, validationErrors);
};