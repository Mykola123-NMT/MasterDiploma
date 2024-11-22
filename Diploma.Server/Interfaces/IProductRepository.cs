using Diploma.Server.Models;

namespace Diploma.Server.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetProductByIdAsync(string id);
        Task<List<Product>> GetProductsAsync();
        Task<List<Product>> GetProductsByPageAsync(int pageNumber, int pageSize);
        Task<bool> ProductExistsAsync(string id);
        Task UpdateProductAsync(Product product);
        Task<Product> AddProductAsync(Product product);
        Task DeleteProductAsync(Product product);
    }
}
