using Catalog.Application.Common.Models;
using Catalog.Application.Features.Products.Shared;
using MediatR;

namespace Catalog.Application.Features.Products.GetProductById;

public record GetProductByIdQuery(string Id) : IRequest<Result<ProductDto>>;