using AutoMapper;
using Catalog.Application.Common.Interfaces;
using Catalog.Application.Common.Models;
using Catalog.Application.Features.Products.Shared;
using MediatR;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Catalog.Application.Features.Products.GetProductsByName;

public class GetProductsByNameQueryHandler(IProductRepository productRepository, IMapper mapper)
  : IRequestHandler<GetProductsByNameQuery, Result<IList<ProductDto>>>
{
  private readonly IMapper _mapper = mapper;
  private readonly IProductRepository _productRepository = productRepository;

  public async Task<Result<IList<ProductDto>>> Handle(GetProductsByNameQuery request, CancellationToken cancellationToken)
  {
    // var product = await _productRepository.SearchByNameAsync(request.Name, cancellationToken);
    // var  productDto = _mapper.Map<IList<ProductDto>>(product);
    // return productDto;

    var products = await _productRepository.GetByNameAsync(request.Name, cancellationToken);
    return  Result<IList<ProductDto>>.Success(_mapper.Map<IList<ProductDto>>(products));
  }
}