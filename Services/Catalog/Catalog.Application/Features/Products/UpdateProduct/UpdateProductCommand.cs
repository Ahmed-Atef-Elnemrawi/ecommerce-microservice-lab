using Catalog.Core.Entities;
using MediatR;

namespace Catalog.Application.Features.Products.UpdateProduct;

public record UpdateProductCommand(
  string Name,
  string Description,
  string Summary,
  decimal Price,
  ProductBrand Brand,
  ProductType Type
  ) : IRequest<bool>;