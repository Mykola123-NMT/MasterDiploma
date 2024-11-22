using Diploma.Server.Interfaces;
using Diploma.Server.Models;
using Diploma.Server.Services;
using Moq;

namespace DiplomaTest
{
    internal class ProductServiceTests
    {
        [Test]
        public async Task GetProductByIdAsync_ReturnsProduct_WhenProductExists()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            var expectedProduct = new Product { Asin = "123" };
            mockRepository.Setup(repo => repo.GetProductByIdAsync("123")).ReturnsAsync(expectedProduct);

            var productService = new ProductService(mockRepository.Object);

            // Act
            var result = await productService.GetProductByIdAsync("123");

            // Assert
            Assert.That(result, Is.EqualTo(expectedProduct));
        }
        [Test]
        public async Task UpdateProductAsync_ReturnsFalse_WhenProductNotFound()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            mockRepository.Setup(repo => repo.GetProductByIdAsync("123")).ReturnsAsync((Product)null);

            var productService = new ProductService(mockRepository.Object);
            var updatedProduct = new Product { Asin = "123" };

            // Act
            var result = await productService.UpdateProductAsync("123", updatedProduct);

            // Assert
            Assert.IsFalse(result);
        }
        [Test]
        public async Task GetProductsAsync_ReturnsListOfProducts()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            var products = new List<Product>
            {
                new Product { Asin = "123" },
                new Product { Asin = "456" }
            };
            mockRepository.Setup(repo => repo.GetProductsAsync()).ReturnsAsync(products);

            var productService = new ProductService(mockRepository.Object);

            // Act
            var result = await productService.GetProductsAsync();

            // Assert
            Assert.That(result.Count, Is.EqualTo(products.Count));
        }
    }
}
