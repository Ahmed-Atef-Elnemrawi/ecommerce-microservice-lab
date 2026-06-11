using AutoMapper;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers.Queries;

public class GetProductByIdQueryHandler(IProductRepository productRepository, IMapper mapper)
  : IRequestHandler<GetProductByIdQuery, ProductDto?>
{
  private readonly IProductRepository _productRepository = productRepository;
  private readonly IMapper _mapper = mapper;

  public async Task<ProductDto?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
  {
    var product = await _productRepository.GetByIdAsync(request.Id,  cancellationToken);
    var productDto = _mapper.Map<ProductDto>(product);
    return productDto;
  }
}