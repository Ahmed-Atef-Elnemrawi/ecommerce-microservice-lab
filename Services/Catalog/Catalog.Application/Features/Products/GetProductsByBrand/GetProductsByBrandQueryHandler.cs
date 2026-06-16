using AutoMapper;
using Catalog.Application.Common.Interfaces;
using Catalog.Application.Common.Models;
using Catalog.Application.Features.Products.Shared;
using MediatR;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Catalog.Application.Features.Products.GetProductsByBrand;

public class GetProductsByBrandQueryHandler(
  ICatalogDbContext dbContext)
  : IRequestHandler<GetProductsByBrandQuery, Result<IList<ProductDto>>>
{
  // private readonly IProductBrandRepository _productBrandRepository = productBrandRepository;
  // private readonly IProductRepository _productRepository = productRepository;
  // private readonly IMapper _mapper = mapper;

  private readonly ICatalogDbContext _dbContext = dbContext;

  public async Task<Result<IList<ProductDto>>> Handle(GetProductsByBrandQuery request, CancellationToken cancellationToken)
  {
    // var brand = await _productBrandRepository.GetByNameAsync(request.Name, cancellationToken);
    //
    // if (brand is null)
    //   return Enumerable.Empty<ProductDto>().ToList();
    //
    // var result = await _productRepository.GetByBrandAsync(brand.Id, cancellationToken);
    // return _mapper.Map<IList<ProductDto>>(result);

    var productsDto = await _dbContext.Products.AsQueryable().Where(p => p.Brand.Name == request.Name)
      .Select(p => p.MapToProductDto()).ToListAsync(cancellationToken);

    return Result<IList<ProductDto>>.Success(productsDto);
  }
}