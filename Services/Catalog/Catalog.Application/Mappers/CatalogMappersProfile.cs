using AutoMapper;
using Catalog.Application.Responses;
using Catalog.Core.Entities;

namespace Catalog.Application.Mappers;

public class CatalogMappersProfile : Profile
{
   public CatalogMappersProfile()
   {
      CreateMap<ProductBrand, ProductBrandDto>().ReverseMap();
      CreateMap<Product, ProductDto>().ReverseMap();
      CreateMap<ProductType, ProductTypeDto>().ReverseMap();
   }
}