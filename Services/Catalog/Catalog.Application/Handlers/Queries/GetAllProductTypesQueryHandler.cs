using AutoMapper;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers.Queries;

public class GetAllProductTypesQueryHandler(IProductTypeRepository productTypeRepository, IMapper mapper)
  : IRequestHandler<GetAllProductTypesQuery, IList<ProductTypeDto>>
{
  private readonly IProductTypeRepository _productTypeRepository = productTypeRepository;
  private readonly IMapper _mapper = mapper;

  public async Task<IList<ProductTypeDto>> Handle(GetAllProductTypesQuery request, CancellationToken cancellationToken)
  {
    var productTypeList = await _productTypeRepository.GetAllProductTypes(cancellationToken);
    return _mapper.Map<IList<ProductTypeDto>>(productTypeList);
  }
}