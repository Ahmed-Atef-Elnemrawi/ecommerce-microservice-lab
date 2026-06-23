using Catalog.Application.Common.Models;
using Catalog.Application.Features.Products.Shared;
using Catalog.Core.helpers;
using MediatR;

namespace Catalog.Application.Features.Products.GetProducts;

public record GetProductsQuery(QueryParams QueryParams) : IRequest<Result<PaginatedList<ProductDto>>>;