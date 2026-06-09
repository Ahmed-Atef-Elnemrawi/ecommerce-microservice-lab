using AutoMapper;
using Catalog.Application.Commands;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers.Commands;

public class CreateProductCommandHandlers(IProductRepository productRepository, IMapper mapper)
  : IRequestHandler<CreateProductCommand, ProductDto>
{
  public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
  {
    var productEntity = mapper.Map<Product>(request);
    var newProduct = await productRepository.CreateProduct(productEntity, cancellationToken);
    var productDto = mapper.Map<ProductDto>(newProduct);

    return productDto;
  }
}