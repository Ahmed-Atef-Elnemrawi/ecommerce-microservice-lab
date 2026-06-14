using MediatR;

namespace Catalog.Application.Features.Products.DeleteProduct;

public record DeleteProductCommand(string ProductId) : IRequest<bool>;