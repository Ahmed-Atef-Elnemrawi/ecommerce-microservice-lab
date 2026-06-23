using AutoMapper;
using Catalog.Application.Common.Interfaces;
using Catalog.Application.Common.Models;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;
using MongoDB.Driver;

namespace Catalog.Application.Features.Types.GetTypes;

public class GetTypesQueryHandler(IProductTypeRepository typeRepository, IMapper mapper)
  : IRequestHandler<GetTypesQuery, Result<IList<TypeDto>>>
{
  private readonly IMapper _mapper = mapper;
  private readonly IProductTypeRepository _repository = typeRepository;

  public async Task<Result<IList<TypeDto>>> Handle(GetTypesQuery request, CancellationToken cancellationToken)
  {
    // var productTypeList = await _productTypeRepository.GetAllAsync(cancellationToken);
    // return _mapper.Map<IList<ProductTypeDto>>(productTypeList);

    var filter = Builders<ProductType>.Filter.Empty;

    var types = await  _repository.GetAllAsync(cancellationToken);
    return Result<IList<TypeDto>>.Success(_mapper.Map<IList<TypeDto>>(types));
  }
}