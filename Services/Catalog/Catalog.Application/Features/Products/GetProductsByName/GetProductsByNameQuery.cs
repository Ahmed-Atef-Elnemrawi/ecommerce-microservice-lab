using Catalog.Application.Common.Models;
using Catalog.Application.Features.Products.Shared;
using MediatR;

namespace Catalog.Application.Features.Products.GetProductsByName;

public record GetProductsByNameQuery(string Name) : IRequest<Result<IList<ProductDto>>>;