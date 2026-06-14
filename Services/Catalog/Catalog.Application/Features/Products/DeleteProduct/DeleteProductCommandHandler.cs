using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Features.Products.DeleteProduct;

public class DeleteProductCommandHandler(IProductRepository productRepository)
  : IRequestHandler<DeleteProductCommand, bool>
{
  public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
  {
      var isDeleted = await productRepository.DeleteAsync(request.ProductId, cancellationToken);

      return isDeleted;
  }
}