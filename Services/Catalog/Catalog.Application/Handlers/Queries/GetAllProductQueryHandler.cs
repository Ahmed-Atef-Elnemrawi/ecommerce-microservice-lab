using AutoMapper;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers.Queries;

public class GetAllProductQueryHandler(IProductRepository productRepository, IMapper mapper)
  : IRequestHandler<GetAllProductsQuery, IList<ProductDto>>
{
  private readonly IProductRepository _productRepository = productRepository;
  private readonly IMapper _mapper = mapper;

  public async Task<IList<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
  {
    var productList = await _productRepository.GetAllAsync(cancellationToken);
    var  productListDto = _mapper.Map<IList<ProductDto>>(productList);

    return productListDto;
  }
}