using AutoMapper;
using Catalog.Application.Commands;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers.Commands;

public class UpdateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
  : IRequestHandler<UpdateProductCommand, bool>
{
  public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
  {
    var productEntity = mapper.Map<Product>(request);
    var isUpdated = await productRepository.UpdateProduct(productEntity, cancellationToken);


    return isUpdated;
  }
}