using AutoMapper;
using Catalog.Application.Features.Brands.GetBrands;
using Catalog.Application.Features.Products.Shared;
using Catalog.Application.Features.Types.GetTypes;
using Catalog.Core.Entities;

namespace Catalog.Application.Mappers;

public class CatalogMappersProfile : Profile
{
   public CatalogMappersProfile()
   {
      CreateMap<ProductBrand, ProductBrandDto>().ReverseMap();
      CreateMap<Product, ProductDto>().ReverseMap();
      CreateMap<ProductType, ProductTypeDto>().ReverseMap();
      CreateMap<ProductType, TypeDto>().ReverseMap();
      CreateMap<ProductBrand, BrandDto>().ReverseMap();
   }
}