using AutoMapper;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers.Queries;

public class GetAllProductBrandsQueryHandler(IProductBrandRepository productBrandRepository, IMapper mapper)
  : IRequestHandler<GetAllProductBrandsQuery, IList<ProductBrandDto>>
{
  private readonly IProductBrandRepository _productBrandRepository = productBrandRepository;
  private readonly IMapper _mapper = mapper;

  public async Task<IList<ProductBrandDto>> Handle(GetAllProductBrandsQuery request,
    CancellationToken cancellationToken)
  {
    var brands = await _productBrandRepository.GetAllProductBrands(cancellationToken);
    var brandsListDto = _mapper.Map<IList<ProductBrandDto>>(brands);
    return brandsListDto;
  }
}