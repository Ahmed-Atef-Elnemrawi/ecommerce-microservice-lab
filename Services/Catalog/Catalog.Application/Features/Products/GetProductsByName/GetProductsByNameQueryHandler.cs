using Catalog.Application.Common.Interfaces;
using Catalog.Application.Common.Models;
using Catalog.Application.Features.Products.Shared;
using MediatR;
using Catalog.Core.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Catalog.Application.Features.Products.GetProductsByName;

public class GetProductsByNameQueryHandler(ICatalogDbContext dbContext)
  : IRequestHandler<GetProductsByNameQuery, Result<IList<ProductDto>>>
{
  // private readonly IProductRepository _productRepository = productRepository;
  // private readonly IMapper _mapper = mapper;

  private readonly ICatalogDbContext _dbContext = dbContext;

  public async Task<Result<IList<ProductDto>>> Handle(GetProductsByNameQuery request, CancellationToken cancellationToken)
  {
    // var product = await _productRepository.SearchByNameAsync(request.Name, cancellationToken);
    // var  productDto = _mapper.Map<IList<ProductDto>>(product);
    // return productDto;

    var filter = Builders<Product>.Filter.Regex(p => request.Name, new BsonRegularExpression(request.Name, "i"));

    var productsDto = await _dbContext.Products.Find(filter)
      .Project(p => p.MapToProductDto()).ToListAsync(cancellationToken);

    return  Result<IList<ProductDto>>.Success(productsDto);
  }
}