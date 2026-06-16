using Catalog.Application.Common.Models;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Features.Products.DeleteProduct;

public class DeleteProductCommandHandler(IProductRepository productRepository)
  : IRequestHandler<DeleteProductCommand, Result<Unit>>
{
  public async Task<Result<Unit>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
  {
    var isDeleted = await productRepository.DeleteAsync(request.ProductId, cancellationToken);

    if (!isDeleted)
      return Result<Unit>.Failure("Product.Delete", $"Failed to delete product  {request.ProductId}",
        ErrorType.Failure);

    return Result<Unit>.Success(Unit.Value);
  }
}