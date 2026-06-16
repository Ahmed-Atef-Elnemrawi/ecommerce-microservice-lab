using Catalog.Application.Common.Interfaces;
using Catalog.Application.Common.Models;
using Catalog.Core.Entities;
using MediatR;
using MongoDB.Driver;

namespace Catalog.Application.Features.Types.GetTypes;

public class GetTypesQueryHandler(ICatalogDbContext dbContext)
  : IRequestHandler<GetTypesQuery, Result<IList<TypeDto>>>
{
  // private readonly IProductTypeRepository _productTypeRepository = productTypeRepository;
  // private readonly IMapper _mapper = mapper;

  private readonly ICatalogDbContext _dbContext = dbContext;

  public async Task<Result<IList<TypeDto>>> Handle(GetTypesQuery request, CancellationToken cancellationToken)
  {
    // var productTypeList = await _productTypeRepository.GetAllAsync(cancellationToken);
    // return _mapper.Map<IList<ProductTypeDto>>(productTypeList);

    var filter = Builders<ProductType>.Filter.Empty;

    var typesDto = await  _dbContext.ProductTypes.Find(filter).Project(p => new TypeDto
    {
      Id = p.Id,
      Name = p.Name
    }).ToListAsync(cancellationToken);

    return Result<IList<TypeDto>>.Success(typesDto);
  }
}