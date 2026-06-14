using Catalog.Application.Features.Products.Shared;
using MediatR;

namespace Catalog.Application.Features.Products.GetProductById;

public record GetProductByIdQuery(string Id) : IRequest<ProductDto?>;