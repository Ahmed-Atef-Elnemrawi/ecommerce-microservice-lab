using AutoMapper;
using Catalog.Application.Common.Interfaces;
using Catalog.Application.Common.Models;
using Catalog.Application.Features.Products.Shared;
using Catalog.Core.Repositories;
using MediatR;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Catalog.Application.Features.Products.GetProductsByBrand;

public class GetProductsByBrandQueryHandler(IProductRepository productRepository, IMapper  mapper)
  : IRequestHandler<GetProductsByBrandQuery, Result<IList<ProductDto>>>
{
  // private readonly IProductBrandRepository _productBrandRepository = productBrandRepository;
  // private readonly IProductRepository _productRepository = productRepository;
  private readonly IMapper _mapper = mapper;

  private readonly IProductRepository  _productRepository = productRepository ;

  public async Task<Result<IList<ProductDto>>> Handle(GetProductsByBrandQuery request, CancellationToken cancellationToken)
  {
    // var brand = await _productBrandRepository.GetByNameAsync(request.Name, cancellationToken);
    //
    // if (brand is null)
    //   return Enumerable.Empty<ProductDto>().ToList();
    //
    // var result = await _productRepository.GetByBrandAsync(brand.Id, cancellationToken);
    // return _mapper.Map<IList<ProductDto>>(result);

    var products = await _productRepository.GetByBrandAsync(request.Name, cancellationToken);
    var productsDto = products.Select(p => p.MapToProductDto()).ToList();
    return Result<IList<ProductDto>>.Success(_mapper.Map<IList<ProductDto>>(productsDto));
  }
}