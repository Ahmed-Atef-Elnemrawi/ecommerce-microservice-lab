namespace Catalog.Application.Common.Models;


public record ValidationError(string Field, IReadOnlyList<string>  Messages);