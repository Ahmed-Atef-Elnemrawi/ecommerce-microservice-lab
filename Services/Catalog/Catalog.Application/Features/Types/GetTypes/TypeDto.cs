namespace Catalog.Application.Features.Types.GetTypes;

public record TypeDto
{
  public required string Id { get; init; }
  public required string Name { get; init; }
}