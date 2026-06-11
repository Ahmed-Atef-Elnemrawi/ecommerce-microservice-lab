using Catalog.Application.Responses;
using Catalog.Core.Entities;
using MediatR;

namespace Catalog.Application.Commands;

public record UpdateProductCommand(
  string Name,
  string Description,
  string Summary,
  decimal Price,
  string TypeId,
  string BrandId
  ) : IRequest<bool>;