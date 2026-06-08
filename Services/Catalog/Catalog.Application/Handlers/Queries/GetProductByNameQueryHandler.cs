using AutoMapper;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers.Queries;

public class GetProductByNameQueryHandler(IProductRepository productRepository, IMapper mapper)
  : IRequestHandler<GetProductByNameQuery, IList<ProductDto>>
{
  private readonly IProductRepository _productRepository = productRepository;
  private readonly IMapper _mapper = mapper;

  public async Task<IList<ProductDto>> Handle(GetProductByNameQuery request, CancellationToken cancellationToken)
  {
    var product = await _productRepository.GetAllProductsByName(request.Name);
    var  productDto = _mapper.Map<IList<ProductDto>>(product);
    return productDto;
  }
}