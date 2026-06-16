namespace Catalog.Application.Common.Models;


public record ValidationError(string PropertyName, IReadOnlyList<string>  ErrorMessages);