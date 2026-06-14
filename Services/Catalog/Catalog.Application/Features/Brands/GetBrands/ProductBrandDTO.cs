namespace Catalog.Application.Features.Brands.GetBrands;

public record ProductBrandDto
{
  public required string Id { get; init; }
  public required string Name { get; init; }
};