using AutoMapper;
using Catalog.Application.Common.Interfaces;
using Catalog.Application.Common.Models;
using Catalog.Application.Features.Products.Shared;
using MediatR;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Catalog.Application.Features.Products.GetProductById;

public class GetProductByIdQueryHandler(ICatalogDbContext dbContext)
  : IRequestHandler<GetProductByIdQuery, Result<ProductDto>>
{
  private readonly ICatalogDbContext _dbContext = dbContext;

  public async Task<Result<ProductDto>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
  {
    // var product = await _productRepository.GetByIdAsync(request.Id,  cancellationToken);
    // var productDto = _mapper.Map<ProductDto>(product);
    // return productDto;

    var productDto = await _dbContext.Products.AsQueryable().Where(p => p.Id == request.Id)
      .Select(p => p.MapToProductDto())
      .FirstOrDefaultAsync(cancellationToken);


    if (productDto is null)
      return Result<ProductDto>.Failure("product", $"Product With ID '{request.Id} was not found'",
        ErrorType.NotFound);


    return Result<ProductDto>.Success(productDto);
  }
}