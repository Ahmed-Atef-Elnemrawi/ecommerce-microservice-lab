using Catalog.Application.Features.Products.Shared;
using MediatR;

namespace Catalog.Application.Features.Products.GetProducts;

public record GetProductsQuery :  IRequest<IList<ProductDto>>;