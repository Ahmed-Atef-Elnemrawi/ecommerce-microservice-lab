using Catalog.Application.Commands;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers.Commands;

public class DeleteProductCommandHandler(IProductRepository productRepository)
  : IRequestHandler<DeleteProductCommand, bool>
{
  public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
  {
      var isDeleted = await productRepository.DeleteProduct(request.ProductId, cancellationToken);

      return isDeleted;
  }
}