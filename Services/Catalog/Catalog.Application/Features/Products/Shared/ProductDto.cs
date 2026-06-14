
namespace Catalog.Application.Features.Products.Shared;

public record ProductDto
{
   public required string Id { get; init; }
   public required string Name { get; init; }
   public required string Description { get; init; }
   public required string Summary { get; init; }
   public required decimal Price { get; init; }
   public required ProductBrandDto Brand { get; init; }
   public required ProductTypeDto Type { get; init; }
};