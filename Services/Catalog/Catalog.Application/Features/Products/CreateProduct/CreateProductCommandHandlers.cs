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
    var entity = Product.Create(request.Name, request.Description, request.Summary, request.Price,
      request.Brand, request.Type);

    var newProduct = await productRepository.AddAsync(productEntity, cancellationToken);
    var productDto = mapper.Map<ProductDto>(newProduct);

    return productDto;
  }
}