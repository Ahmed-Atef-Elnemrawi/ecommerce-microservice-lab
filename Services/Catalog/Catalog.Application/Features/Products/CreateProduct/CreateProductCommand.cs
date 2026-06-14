using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Commands;

public record CreateProductCommand(
  string Name,
  string Description,
  string Summary,
  decimal Price,
  ProductBrand Brand,
  ProductType Type) : IRequest<ProductDto>;