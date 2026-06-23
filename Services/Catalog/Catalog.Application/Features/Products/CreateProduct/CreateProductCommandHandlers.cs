using AutoMapper;
using Catalog.Application.Common.Models;
using Catalog.Application.Features.Products.Shared;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Features.Products.CreateProduct;

public class CreateProductCommandHandlers(
  IProductRepository productRepository,
  IProductBrandRepository brandRepository,
  IProductTypeRepository typeRepository,
  IMapper mapper)
  : IRequestHandler<CreateProductCommand, Result<ProductDto>>
{
  public async Task<Result<ProductDto>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
  {
    var typeEntity = await typeRepository.GetByIdAsync(request.TypeId, cancellationToken);
    var brandEntity = await brandRepository.GetByIdAsync(request.BrandId, cancellationToken);

    if (typeEntity is null)
      return Result<ProductDto>.Failure("Type.NotExist", "Type is not found", ErrorType.NotFound);

    if (brandEntity is null)
      return Result<ProductDto>.Failure("Brand.NotExist", "Brand is not found", ErrorType.NotFound);

    var entity = Product.Create(request.Name, request.Description, request.Summary, request.Price,
      brandEntity, typeEntity);

    var newProduct = await productRepository.AddAsync(entity, cancellationToken);

    return Result<ProductDto>.Success(mapper.Map<ProductDto>(newProduct));
  }
}