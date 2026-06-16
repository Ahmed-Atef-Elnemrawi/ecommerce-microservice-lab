using Catalog.Application.Common.Models;
using Catalog.Core.Entities;
using MediatR;

namespace Catalog.Application.Features.Products.UpdateProduct;

public record UpdateProductCommand(
  string Name,
  string Description,
  string Summary,
  decimal Price,
  string BrandId,
  string TypeId
  ) : IRequest<Result<Unit>>;