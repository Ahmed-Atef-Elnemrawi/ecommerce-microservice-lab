using Catalog.Application.Features.Products.Shared;
using Catalog.Core.Entities;
using MediatR;

namespace Catalog.Application.Features.Products.CreateProduct;

public record CreateProductCommand(
  string Name,
  string Description,
  string Summary,
  decimal Price,
  ProductBrand Brand,
  ProductType Type) : IRequest<ProductDto>;