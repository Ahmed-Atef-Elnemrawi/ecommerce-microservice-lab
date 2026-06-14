using Catalog.Application.Common.Interfaces;
using Catalog.Core.Entities;
using MediatR;
using MongoDB.Driver;

namespace Catalog.Application.Features.Types.GetTypes;

public class GetTypesQueryHandler(ICatalogDbContext dbContext)
  : IRequestHandler<GetTypesQuery, IList<ProductTypeDto>>
{
  // private readonly IProductTypeRepository _productTypeRepository = productTypeRepository;
  // private readonly IMapper _mapper = mapper;

  private readonly ICatalogDbContext _dbContext = dbContext;

  public async Task<IList<ProductTypeDto>> Handle(GetTypesQuery request, CancellationToken cancellationToken)
  {
    // var productTypeList = await _productTypeRepository.GetAllAsync(cancellationToken);
    // return _mapper.Map<IList<ProductTypeDto>>(productTypeList);

    var filter = Builders<ProductType>.Filter.Empty;
    return await  _dbContext.ProductTypes.Find(filter).Project(p => new ProductTypeDto
    {
      Id = p.Id,
      Name = p.Name
    }).ToListAsync(cancellationToken);
  }
}