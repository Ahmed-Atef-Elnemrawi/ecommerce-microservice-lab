using Catalog.Application.Common.Models;
using MediatR;

namespace Catalog.Application.Features.Products.DeleteProduct;

public record DeleteProductCommand(string ProductId) : IRequest<Result<Unit>>;