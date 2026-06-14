namespace Catalog.Application.Features.Types.GetTypes;

public record ProductTypeDto
{
  public required string Id { get; init; }
  public required string Name { get; init; }
}