using Catalog.Core.Entities;

namespace Catalog.Application.Features.Products.Shared;

public static class MappingsExtensions
{
   public static ProductDto MapToProductDto(this Product product)
   {
     return new ProductDto
     {
       Id = product.Id,
       Name = product.Name,
       Description = product.Description,
       Summary =  product.Summary,
       Price = product.Price,
       Brand = new ProductBrandDto
       {
         Name = product.Brand.Name,
         Id = product.Brand.Id,
       },
       Type = new ProductTypeDto
       {
         Id = product.Type.Id,
         Name = product.Type.Name,
       },
     };
   }

   private static ProductBrandDto MapToProductBrandDto(this ProductBrand brand)
   {
     return new ProductBrandDto
     {
       Id = brand.Id,
       Name = brand.Name,
     };
   }


   private static ProductTypeDto MapToProductTypeDto(this ProductType productType)
   {
     return new ProductTypeDto
     {
        Id = productType.Id,
        Name = productType.Name,
     };
   }
}