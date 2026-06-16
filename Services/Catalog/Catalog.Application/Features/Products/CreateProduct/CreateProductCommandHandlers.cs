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
  IProductTypeRepository typeRepository)
  : IRequestHandler<CreateProductCommand, Result<ProductDto>>
{
  public async Task<Result<ProductDto>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
  {
    var entity = Product.Create(request.Name, request.Description, request.Summary, request.Price,
      brandEntity, typeEntity);

    var newProduct = await productRepository.AddAsync(entity, cancellationToken);

    return Result<ProductDto>.Success(newProduct.MapToProductDto());
  }
}