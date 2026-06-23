namespace Catalog.Core.helpers;

public sealed record QueryParams
{
  private int _pageSize = 10;
  private int _pageNumber = 1;
  private const int MaxPageSize = 50;

  public string? BrandId { get; init; }
  public string? TypeId { get; init; }
  public string? Search { get; init; }
  public string? SortBy { get; init; }
  public string? SortDirection { get; init; }

  public int PageSize
  {
    get => _pageSize;
    init => _pageSize = Math.Clamp(value,1, MaxPageSize);
  }

  public int PageNumber
  {
    get => _pageNumber;
    init => _pageNumber = Math.Max(value, 1);
  }
}