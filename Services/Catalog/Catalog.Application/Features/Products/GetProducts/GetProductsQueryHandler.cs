using Catalog.Application.Common.Interfaces;
using Catalog.Application.Features.Products.Shared;
using MediatR;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Catalog.Application.Features.Products.GetProducts;

public class GetProductsQueryHandler(ICatalogDbContext catalogDbContext)
  : IRequestHandler<GetProductsQuery, IList<ProductDto>>
{
  // private readonly IProductRepository _productRepository = productRepository;
  // private readonly IMapper _mapper = mapper;
  private readonly ICatalogDbContext _catalogDbContext = catalogDbContext;

  public async Task<IList<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
  {
    // var productList = await _productRepository.GetAllAsync(cancellationToken);
    // var  productListDto = _mapper.Map<IList<ProductDto>>(productList);
    //
    // return productListDto;

    return await _catalogDbContext.Products.AsQueryable().Select(p => p.MapToProductDto())
      .ToListAsync(cancellationToken);
  }
}