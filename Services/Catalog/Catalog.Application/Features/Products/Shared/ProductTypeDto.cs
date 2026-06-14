namespace Catalog.Application.Features.Products.Shared;

public record ProductTypeDto
{
  public required string Id { get; init; }
  public required string Name { get; init; }
}