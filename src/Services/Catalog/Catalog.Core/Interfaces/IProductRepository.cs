using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.Core.Entities;

namespace Catalog.Core.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetProduct(string id);
        Task<IEnumerable<Product>> GetProductsByName(string name);
        Task<IEnumerable<Product>> GetProductsByCategory(string categoryName);

        Task CreateProduct(Product product);
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(string id);
    }
}