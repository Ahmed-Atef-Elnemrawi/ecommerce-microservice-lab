using Catalog.Core.Entities;

namespace Catalog.Core.Repositories;

public interface IProductRepository
{
   Task<IEnumerable<Product>> GetAllProducts(CancellationToken cancellationToken);
   Task<Product?> GetProductById(string id, CancellationToken cancellationToken);
   Task<IEnumerable<Product>> SearchProductsByName(string name, CancellationToken cancellationToken);
   Task<IEnumerable<Product>> GetAllProductsByBrand(string brand, CancellationToken cancellationToken);
   Task<Product>  CreateProduct(Product product, CancellationToken cancellationToken);
   Task<bool> UpdateProduct(Product product, CancellationToken cancellationToken);
   Task<bool> DeleteProduct(string id, CancellationToken cancellationToken);
}