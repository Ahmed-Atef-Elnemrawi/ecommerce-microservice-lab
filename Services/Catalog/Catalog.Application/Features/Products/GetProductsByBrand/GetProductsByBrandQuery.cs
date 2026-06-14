using Catalog.Application.Features.Products.Shared;
using MediatR;

namespace Catalog.Application.Features.Products.GetProductsByBrand;

public record GetProductsByBrandQuery(string Name):  IRequest<IList<ProductDto>>;