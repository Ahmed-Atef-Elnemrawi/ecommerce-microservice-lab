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
  public async Task<Result<Unit>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
  {
    var typeEntity = await typeRepository.GetByIdAsync(request.TypeId, cancellationToken);
    var brandEntity = await brandRepository.GetByIdAsync(request.BrandId, cancellationToken);

    if (typeEntity is null)
      return Result<Unit>.Failure("Type.NotExist", "Type is not found", ErrorType.NotFound);

    if (brandEntity is null)
      return Result<Unit>.Failure("Brand.NotExist", "Brand is not found", ErrorType.NotFound);

    var productEntity = Product.Create(request.Name, request.Description, request.Summary, request.Price,
      brandEntity, typeEntity, request.Id);

    var isUpdated = await productRepository.UpdateAsync(productEntity, cancellationToken);

    if (!isUpdated)
      return Result<Unit>.Failure("product.Update", "Product is not updated", ErrorType.Failure);

    return Result<Unit>.Success(Unit.Value);
  }
}