namespace Catalog.Application.Features.Brands.GetBrands;

public record BrandDto
{
  public required string Id { get; init; }
  public required string Name { get; init; }
};