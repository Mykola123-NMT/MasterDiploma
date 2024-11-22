using Diploma.Server.Interfaces;
using Diploma.Server.Models;
using Diploma.Server.Repositories;
using Microsoft.EntityFrameworkCore;
using static Diploma_Server.RegressionModel;

namespace Diploma.Server.Services
{
    public class ProductService: IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Product> GetProductByIdAsync(string id)
        {
            return await _repository.GetProductByIdAsync(id);
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            return await _repository.GetProductsAsync();
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            if (await _repository.GetProductByIdAsync(product.Asin) != null)
            {
                throw new DbUpdateException("Product with this ASIN already exists.");
            }

            return await _repository.AddProductAsync(product);
        }

        public async Task<bool> ProductExistsAsync(string id)
        {
            return await _repository.ProductExistsAsync(id);
        }
        public async Task<bool> UpdateProductAsync(string id, Product updatedProduct)
        {
            var existingProduct = await _repository.GetProductByIdAsync(id);
            if (existingProduct == null)
            {
                return false; // Продукт не знайдено
            }

            // Оновлення властивостей продукту
            existingProduct.Title = updatedProduct.Title;
            existingProduct.Price = updatedProduct.Price;
            existingProduct.ImgUrl = updatedProduct.ImgUrl;
            existingProduct.ProductUrl = updatedProduct.ProductUrl;
            existingProduct.Stars = updatedProduct.Stars;
            existingProduct.Reviews = updatedProduct.Reviews;
            existingProduct.Price = updatedProduct.Price;
            existingProduct.ListPrice = updatedProduct.ListPrice;
            existingProduct.CategoryId = updatedProduct.CategoryId;
            existingProduct.IsBestSeller = updatedProduct.IsBestSeller;
            existingProduct.BoughtLastMonth = updatedProduct.BoughtLastMonth;
            existingProduct.Discount = updatedProduct.Discount;
            existingProduct.LogReviews = updatedProduct.LogReviews;
            existingProduct.LogPrice = updatedProduct.LogPrice;
            existingProduct.ExpertEvaluations = updatedProduct.ExpertEvaluations;
            existingProduct.ConsensusEvaluation = updatedProduct.ConsensusEvaluation;

            await _repository.UpdateProductAsync(existingProduct);
            return true;
        }

        public async Task<bool> DeleteProductAsync(string id)
        {
            var product = await _repository.GetProductByIdAsync(id);
            if (product == null)
            {
                return false; // Продукт не знайдено
            }

            await _repository.DeleteProductAsync(product);
            return true;
        }

        public async Task<List<Product>> GetProductsByPageAsync(int pageNumber, int pageSize)
        {
            return await _repository.GetProductsByPageAsync(pageNumber, pageSize);
        }

        public ModelInput GetRegressionModelInput(ConsensusEvaluation consensusEvaluation)
        {
            ModelInput modelInput = new ModelInput()
            {
                Stars = (float)consensusEvaluation.Product.Stars,
                BoughtInLastMonth = (float)consensusEvaluation.Product.BoughtLastMonth,
                Discount = (float)consensusEvaluation.Product.Discount,
                Log_price = (float)consensusEvaluation.Product.LogPrice,
                Log_reviews = (float)consensusEvaluation.Product.LogReviews,
                PriceStrategy = (float)consensusEvaluation.PriceStrategy,
                Demand = (float)consensusEvaluation.Demand,
                Quality = (float)consensusEvaluation.Quality,
                PriceQuality = (float)consensusEvaluation.PriceQuality
            };
            return modelInput;
        }
    }
}
