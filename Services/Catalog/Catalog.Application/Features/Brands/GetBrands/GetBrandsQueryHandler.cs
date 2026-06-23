using AutoMapper;
using Catalog.Application.Common.Interfaces;
using Catalog.Application.Common.Models;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;
using MongoDB.Driver;

namespace Catalog.Application.Features.Brands.GetBrands;

public class GetAllProductBrandsQueryHandler(IProductBrandRepository brandRepository, IMapper mapper)
  : IRequestHandler<GetBrandsQuery, Result<IList<BrandDto>>>
{
  private readonly IProductBrandRepository _productBrandRepository = brandRepository;
  private readonly IMapper _mapper = mapper;

  // private readonly ICatalogDbContext _dbContext = dbContext;

  public async Task<Result<IList<BrandDto>>> Handle(GetBrandsQuery request, CancellationToken cancellationToken)
  {
    var filter = Builders<ProductBrand>.Filter.Empty;

    var brands = await _productBrandRepository.GetAllAsync(cancellationToken);
    return Result<IList<BrandDto>>.Success(_mapper.Map<List<BrandDto>>(brands));
  }
}