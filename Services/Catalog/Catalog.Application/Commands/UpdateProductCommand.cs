using Catalog.Application.Responses;
using Catalog.Core.Entities;
using MediatR;

namespace Catalog.Application.Commands;

public record UpdateProductCommand(
  string Name,
  string Description,
  string Summary,
  decimal Price,
  ProductType Type,
  ProductBrand Brand
  ) : IRequest<bool>;