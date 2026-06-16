using Catalog.Application.Common.Models;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Features.Products.UpdateProduct;

public class UpdateProductCommandHandler(
  IProductRepository productRepository,
  IProductTypeRepository typeRepository,
  IProductBrandRepository brandRepository)
  : IRequestHandler<UpdateProductCommand, Result<Unit>>
{
  public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
  {
    var productEntity = Product.Create(request.Name, request.Description, request.Summary, request.Price,
      brandEntity, typeEntity);

    var isUpdated = await productRepository.UpdateAsync(productEntity, cancellationToken);

    if (!isUpdated)
      return Result<Unit>.Failure("product.Update", "Product is not updated", ErrorType.Failure);

    return Result<Unit>.Success(Unit.Value);
  }
}