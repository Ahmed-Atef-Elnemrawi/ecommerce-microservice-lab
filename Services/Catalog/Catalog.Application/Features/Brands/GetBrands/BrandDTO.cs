namespace Catalog.Application.Features.Brands.GetBrands;

public record struct BrandDto
{
  public required string Id { get; init; }
  public required string Name { get; init; }
};