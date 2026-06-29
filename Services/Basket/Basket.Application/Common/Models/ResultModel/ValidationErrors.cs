namespace Basket.Application.Common.Models.ResultModel;

public record ValidationError(string Field, IReadOnlyList<string> Messages);