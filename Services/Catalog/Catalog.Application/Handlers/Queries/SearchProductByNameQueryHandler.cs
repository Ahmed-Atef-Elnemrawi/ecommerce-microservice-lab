using AutoMapper;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers.Queries;

public class SearchProductsByNameQueryHandler(IProductRepository productRepository, IMapper mapper)
  : IRequestHandler<SearchProductsByNameQuery, IList<ProductDto>>
{
  private readonly IProductRepository _productRepository = productRepository;
  private readonly IMapper _mapper = mapper;

  public async Task<IList<ProductDto>> Handle(SearchProductsByNameQuery request, CancellationToken cancellationToken)
  {
    var product = await _productRepository.SearchByNameAsync(request.Name, cancellationToken);
    var  productDto = _mapper.Map<IList<ProductDto>>(product);
    return productDto;
  }
}