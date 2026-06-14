using Catalog.Application.Common.Interfaces;
using Catalog.Core.Entities;
using MediatR;
using MongoDB.Driver;

namespace Catalog.Application.Features.Brands.GetBrands;

public class GetAllProductBrandsQueryHandler(ICatalogDbContext dbContext)
  : IRequestHandler<GetBrandsQuery, IList<ProductBrandDto>>
{
  // private readonly IProductBrandRepository _productBrandRepository = productBrandRepository;
  // private readonly IMapper _mapper = mapper;

  private readonly ICatalogDbContext _dbContext = dbContext;

  public async Task<IList<ProductBrandDto>> Handle(GetBrandsQuery request, CancellationToken cancellationToken)
  {
    var filter = Builders<ProductBrand>.Filter.Empty;

    return await  _dbContext.ProductBrands.Find(filter).Project(p => new ProductBrandDto
    {
      Id = p.Id,
      Name = p.Name
    }).ToListAsync(cancellationToken);
  }
}