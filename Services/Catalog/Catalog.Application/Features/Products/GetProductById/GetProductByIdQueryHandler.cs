using AutoMapper;
using Catalog.Application.Common.Interfaces;
using Catalog.Application.Common.Models;
using Catalog.Application.Features.Products.Shared;
using Catalog.Core.Repositories;
using MediatR;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Catalog.Application.Features.Products.GetProductById;

public class GetProductByIdQueryHandler(IProductRepository productRepository, IMapper mapper)
  : IRequestHandler<GetProductByIdQuery, Result<ProductDto>>
{
  private readonly IProductRepository  _productRepository = productRepository;
  private readonly IMapper _mapper = mapper;

  public async Task<Result<ProductDto>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
  {
    // var product = await _productRepository.GetByIdAsync(request.Id,  cancellationToken);
    // var productDto = _mapper.Map<ProductDto>(product);
    // return productDto;

    var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);


    if (product is null)
      return Result<ProductDto>.Failure("product", $"Product With ID '{request.Id} was not found'",
        ErrorType.NotFound);


    return Result<ProductDto>.Success(_mapper.Map<ProductDto>(product));
  }
}