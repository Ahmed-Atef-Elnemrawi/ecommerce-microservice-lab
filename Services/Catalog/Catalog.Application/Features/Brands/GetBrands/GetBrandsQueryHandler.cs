using Catalog.Application.Common.Interfaces;
using Catalog.Application.Common.Models;
using Catalog.Core.Entities;
using MediatR;
using MongoDB.Driver;

namespace Catalog.Application.Features.Brands.GetBrands;

public class GetAllProductBrandsQueryHandler(ICatalogDbContext dbContext)
  : IRequestHandler<GetBrandsQuery, Result<IList<BrandDto>>>
{
  // private readonly IProductBrandRepository _productBrandRepository = productBrandRepository;
  // private readonly IMapper _mapper = mapper;

  private readonly ICatalogDbContext _dbContext = dbContext;

  public async Task<Result<IList<BrandDto>>> Handle(GetBrandsQuery request, CancellationToken cancellationToken)
  {
    var filter = Builders<ProductBrand>.Filter.Empty;

    var brandsDto = await  _dbContext.ProductBrands.Find(filter).Project(p => new BrandDto
    {
      Id = p.Id,
      Name = p.Name
    }).ToListAsync(cancellationToken);

    return Result<IList<BrandDto>>.Success(brandsDto);
  }
}