using AutoMapper;
using Catalog.Application.Common.Interfaces;
using Catalog.Application.Features.Products.Shared;
using Catalog.Core.Repositories;
using MediatR;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Catalog.Application.Features.Products.GetProductById;

public class GetProductByIdQueryHandler(ICatalogDbContext dbContext, IMapper mapper)
  : IRequestHandler<GetProductByIdQuery, ProductDto?>
{
  private readonly ICatalogDbContext _dbContext = dbContext;

  public async Task<ProductDto?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
  {
    // var product = await _productRepository.GetByIdAsync(request.Id,  cancellationToken);
    // var productDto = _mapper.Map<ProductDto>(product);
    // return productDto;

    return await _dbContext.Products.AsQueryable().Where(p => p.Id == request.Id).Select(p => p.MapToProductDto())
      .FirstOrDefaultAsync(cancellationToken);
  }
}