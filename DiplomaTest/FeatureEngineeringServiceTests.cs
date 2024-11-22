using Diploma.Server.Interfaces;
using Diploma.Server.Models;
using Diploma.Server.Services;
using Moq;
namespace DiplomaTest
{
    internal class FeatureEngineeringServiceTests
    {
        private Mock<IProductService> _mockProductService;
        private Mock<IExpertEvaluationService> _mockExpertEvaluationService;
        private Mock<IOpinionAgreementService> _mockOpinionAgreementService;

        [SetUp]
        public void Setup()
        {
            _mockProductService = new Mock<IProductService>();
            _mockExpertEvaluationService = new Mock<IExpertEvaluationService>();
            _mockOpinionAgreementService = new Mock<IOpinionAgreementService>();
        }

        [Test]
        public async Task EvaluateAllProductsAsync_ShouldProcessInBatches()
        {
            // Arrange         

            var products = new List<Product>
            {
                new Product { Asin = "1" },
                new Product { Asin = "2" },
                new Product { Asin = "3" },
                new Product { Asin = "4" },
                new Product { Asin = "5" }
            };

            _mockProductService.Setup(s => s.GetProductsAsync()).ReturnsAsync(products);
            _mockExpertEvaluationService.Setup(s => s.GetOpinionsAsync(It.IsAny<Product>())).ReturnsAsync(new List<ExpertEvaluation>());
            _mockOpinionAgreementService.Setup(s => s.GenerateConsensusOpinion(It.IsAny<List<ExpertEvaluation>>())).ReturnsAsync(It.IsAny<ConsensusEvaluation>);

            var service = new FeatureEngineeringService(_mockProductService.Object, _mockExpertEvaluationService.Object, _mockOpinionAgreementService.Object);

            // Act
            await service.EvaluateAllProductsAsync();

            // Assert
            _mockProductService.Verify(s => s.GetProductsAsync(), Times.Once);
            _mockProductService.Verify(s => s.UpdateProductAsync(It.IsAny<string>(), It.IsAny<Product>()), Times.Exactly(products.Count));
            _mockExpertEvaluationService.Verify(s => s.GetOpinionsAsync(It.IsAny<Product>()), Times.Exactly(products.Count));
            _mockOpinionAgreementService.Verify(s => s.GenerateConsensusOpinion(It.IsAny<List<ExpertEvaluation>>()), Times.Exactly(products.Count));
        }

        [Test]
        public async Task EvaluateAllProductsAsync_ShouldCallRequiredMethodsForEachProduct()
        {

            var products = new List<Product>
            {
                new Product { Asin = "1" },
                new Product { Asin = "2" }
            };

            _mockProductService.Setup(s => s.GetProductsAsync()).ReturnsAsync(products);
            _mockExpertEvaluationService.Setup(s => s.GetOpinionsAsync(It.IsAny<Product>())).ReturnsAsync(new List<ExpertEvaluation>());
            _mockOpinionAgreementService.Setup(s => s.GenerateConsensusOpinion(It.IsAny<List<ExpertEvaluation>>())).ReturnsAsync(It.IsAny<ConsensusEvaluation>);

            var service = new FeatureEngineeringService(_mockProductService.Object, _mockExpertEvaluationService.Object, _mockOpinionAgreementService.Object);

            // Act
            await service.EvaluateAllProductsAsync();

            // Assert
            _mockExpertEvaluationService.Verify(s => s.GetOpinionsAsync(It.IsAny<Product>()), Times.Exactly(products.Count), "GetOpinionsAsync should be called once for each product.");
            _mockOpinionAgreementService.Verify(s => s.GenerateConsensusOpinion(It.IsAny<List<ExpertEvaluation>>()), Times.Exactly(products.Count), "GenerateConsensusOpinion should be called once for each product.");
            _mockProductService.Verify(s => s.UpdateProductAsync(It.IsAny<string>(), It.IsAny<Product>()), Times.Exactly(products.Count), "UpdateProductAsync should be called once for each product.");
        }
    }
}
