using Catalog.Application.Common.Models;
using Catalog.Application.Features.Products.Shared;
using MediatR;

namespace Catalog.Application.Features.Products.CreateProduct;

public record CreateProductCommand(
  string Name,
  string Description,
  string Summary,
  decimal Price,
  string BrandId,
  string TypeId) : IRequest<Result<ProductDto>>;