using Diploma.Server.Models;
using NuGet.Protocol.Core.Types;
using static Diploma_Server.RegressionModel;

namespace Diploma.Server.Interfaces
{
    public interface IProductService
    {
        Task<Product> GetProductByIdAsync(string id);
        Task<List<Product>> GetProductsAsync();
        Task<List<Product>> GetProductsByPageAsync(int pageNumber, int pageSize);
        Task<bool> ProductExistsAsync(string id);
        Task<bool> UpdateProductAsync(string id, Product product);
        Task<Product> AddProductAsync(Product product);
        ModelInput GetRegressionModelInput(ConsensusEvaluation consensusEvaluation);
        Task<bool> DeleteProductAsync(string id);
    }
}
