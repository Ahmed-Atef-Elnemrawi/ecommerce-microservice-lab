namespace Catalog.Core.helpers;

public sealed class PaginatedList<T>(IReadOnlyList<T> items, int pageNumber, int pageSize, long totalCount)
{
  public int PageNumber { get; } = pageNumber;
  public int PageSize { get; } = pageSize;
  public long TotalCount { get; } = totalCount;
  public IReadOnlyList<T> Items { get; } = items;

  public int TotalPages => PageSize == 0 ? 0 : (int)Math.Ceiling((double)TotalCount / PageSize);
  public bool HasPreviousPage => PageNumber > 1;
  public bool HasNextPage => PageNumber < TotalPages;

  public static PaginatedList<T> Create(IReadOnlyList<T> items, int pageNumber, int pageSize, long totalCount)
  {
    return new PaginatedList<T>(items, pageNumber, pageSize, totalCount);
  }
}