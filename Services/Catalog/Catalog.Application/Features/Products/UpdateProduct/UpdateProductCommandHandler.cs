using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Features.Products.UpdateProduct;

public class UpdateProductCommandHandler(IProductRepository productRepository)
  : IRequestHandler<UpdateProductCommand, bool>
{
  public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
  {
    var productEntity = Product.Create(request.Name, request.Description, request.Summary, request.Price,
      request.Brand, request.Type);

    var isUpdated = await productRepository.UpdateAsync(productEntity, cancellationToken);


    return isUpdated;
  }
}