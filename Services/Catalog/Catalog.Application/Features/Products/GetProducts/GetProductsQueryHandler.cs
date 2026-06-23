using AutoMapper;
using Catalog.Application.Common.Interfaces;
using Catalog.Application.Common.Models;
using Catalog.Application.Features.Products.Shared;
using Catalog.Core.helpers;
using Catalog.Core.Repositories;
using MediatR;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Catalog.Application.Features.Products.GetProducts;

public class GetProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
  : IRequestHandler<GetProductsQuery, Result<PaginatedList<ProductDto>>>
{
  private readonly IProductRepository _productRepository = productRepository;
  private readonly IMapper _mapper = mapper;
  // private readonly ICatalogDbContext _catalogDbContext = catalogDbContext;

  public async Task<Result<PaginatedList<ProductDto>>> Handle(GetProductsQuery request,
    CancellationToken cancellationToken)
  {
    // var productList = await _productRepository.GetAllAsync(cancellationToken);
    // var  productListDto = _mapper.Map<IList<ProductDto>>(productList);
    //
    // return productListDto;

    var result = await _productRepository.GetAllAsync(request.QueryParams, cancellationToken);
    return Result<PaginatedList<ProductDto>>.Success(PaginatedList<ProductDto>.Create(
      _mapper.Map<IReadOnlyList<ProductDto>>(result.Items),
      result.PageNumber,
      result.PageSize,
      result.TotalCount));
  }
}