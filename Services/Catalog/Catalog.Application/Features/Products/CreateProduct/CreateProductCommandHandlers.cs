using AutoMapper;
using Catalog.Application.Features.Products.Shared;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Features.Products.CreateProduct;

public class CreateProductCommandHandlers(IProductRepository productRepository, IMapper mapper)
  : IRequestHandler<CreateProductCommand, ProductDto>
{
  public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
  {
    var entity = Product.Create(request.Name, request.Description, request.Summary, request.Price,
      request.Brand, request.Type);

    var newProduct = await productRepository.AddAsync(entity, cancellationToken);

    return newProduct.MapToProductDto();
  }
}