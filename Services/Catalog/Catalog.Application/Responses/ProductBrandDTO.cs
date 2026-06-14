namespace Catalog.Application.Responses;

public record ProductBrandDto
{
  public required string Id { get; init; }
  public required string Name { get; init; }
};