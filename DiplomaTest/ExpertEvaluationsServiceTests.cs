using Moq;
using System.Net;
using Diploma.Server.Services;
using Diploma.Server.Models;
using Diploma.Server.Interfaces;
using RichardSzalay.MockHttp;
using Moq.Contrib.HttpClient;


namespace DiplomaTest
{
    [TestFixture]
    public class ExpertEvaluationServiceTests
    {
        private Mock<IExpertService> _mockExpertService;
        private HttpClient _httpClient;
        private MockHttpMessageHandler _httpMessageHandler;
        private ExpertEvaluationService _service;

        [SetUp]
        public void Setup()
        {
            _mockExpertService = new Mock<IExpertService>();

            _httpMessageHandler = new MockHttpMessageHandler();
            _httpClient = new HttpClient(_httpMessageHandler)
            {
                BaseAddress = new Uri("https://example.com")
            };

            _service = new ExpertEvaluationService(_httpClient, _mockExpertService.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _httpMessageHandler?.Dispose();
            _httpClient?.Dispose();
        }

        [Test]
        public async Task GetOpinionsAsync_ReturnsExpertEvaluations()
        {
            // Arrange
            var product = new Product { Title = "Test Product", Price = 100.0 };
            var mockExperts = new List<LLMExpert>
            {
            new LLMExpert { Name = "Expert1", URL = "https://api.expert1.com" },
            new LLMExpert { Name = "Expert2", URL = "https://api.expert2.com" }
            };

            _mockExpertService
                .Setup(service => service.GetExpertsAsync())
                .ReturnsAsync(mockExperts);

            // Mock HTTP responses
            _httpMessageHandler.When("https://api.expert1.com")
                .Respond("application/json", "{\"choices\":[{\"message\":{\"content\":\"{\\r\\n  \\\"priceStrategy\\\": 8.0,\\r\\n  \\\"demand\\\": 9.5,\\r\\n  \\\"quality\\\": 7.5}\"}}]}");

            _httpMessageHandler.When("https://api.expert2.com")
                .Respond("application/json", "{\"choices\":[{\"message\":{\"content\":\"{\\r\\n  \\\"priceStrategy\\\": 8.0,\\r\\n  \\\"demand\\\": 9.5,\\r\\n  \\\"quality\\\": 7.5}\"}}]}");

            // Act
            var result = await _service.GetOpinionsAsync(product);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].Expert.Name, Is.EqualTo("Expert1"));
            Assert.That(result[1].Expert.Name, Is.EqualTo("Expert2"));

            _mockExpertService.Verify(service => service.GetExpertsAsync(), Times.Once);
        }

        [Test]
        public void ParseEvaluationResponse_ShouldReturnCorrectEvaluation_ForValidJson()
        {
            // Arrange
            var validJson = "{\"choices\":[{\"message\":{\"content\":\"{\\\"priceStrategy\\\":5.0,\\\"demand\\\":7.0}\"}}]}";
            var service = new ExpertEvaluationService(new HttpClient(), Mock.Of<IExpertService>());

            // Act
            var result = service.ParseEvaluationResponse(validJson);

            // Assert
            Assert.IsNotNull(result, "Результат не повинен бути null для валідного JSON.");
            Assert.That(result.priceStrategy, Is.EqualTo(5.0), "Параметр PriceStrategy має бути 5.0.");
            Assert.That(result.demand, Is.EqualTo(7.0), "Параметр Demand має бути 7.0.");
        }

        [Test]
        public void ParseEvaluationResponse_ShouldReturnNull_ForInvalidJson()
        {
            // Arrange
            var invalidJson = "{\"choices\":[{\"message\":{\"content\":\"Not a JSON object\"}}]}";
            var service = new ExpertEvaluationService(new HttpClient(), Mock.Of<IExpertService>());

            // Act
            var result = service.ParseEvaluationResponse(invalidJson);

            // Assert
            Assert.IsNull(result, "Метод повинен повертати null для некоректного JSON.");
        }

        [Test]
        public async Task GetOpinionsAsync_ShouldSendHttpRequestsToAllExperts()
        {
            // Arrange
            var httpMessageHandler = new Mock<HttpMessageHandler>();
            var httpClient = httpMessageHandler.CreateClient();
            httpClient.Timeout = TimeSpan.FromSeconds(5000);

            var mockExpertService = new Mock<IExpertService>();
            var experts = new List<LLMExpert>
            {
                new LLMExpert { Name = "Expert1", URL = "http://expert1.com" },
                new LLMExpert { Name = "Expert2", URL = "http://expert2.com" }
            };
            mockExpertService.Setup(s => s.GetExpertsAsync()).ReturnsAsync(experts);

            var product = new Product { Title = "Test Product", Price = 100 };
            var service = new ExpertEvaluationService(httpClient, mockExpertService.Object);

            // Налаштування відповідей HTTP-запитів
            httpMessageHandler
                .SetupRequest(HttpMethod.Post, "http://expert1.com")
                .ReturnsResponse(HttpStatusCode.OK, "{\"choices\":[{\"message\":{\"content\":\"{\\\"priceStrategy\\\":5}\"}}]}");

            httpMessageHandler
                .SetupRequest(HttpMethod.Post, "http://expert2.com")
                .ReturnsResponse(HttpStatusCode.OK, "{\"choices\":[{\"message\":{\"content\":\"{\\\"priceStrategy\\\":5}\"}}]}");

            // Act
            var evaluations = await service.GetOpinionsAsync(product);

            // Assert
            Assert.That(evaluations.Count, Is.EqualTo(2), "Повинно бути дві оцінки, одна від кожного експерта.");
            mockExpertService.Verify(s => s.GetExpertsAsync(), Times.Once);
            Assert.That(httpClient.Timeout, Is.EqualTo(TimeSpan.FromSeconds(5000)), "Таймаут HTTP-клієнта має бути 5000 секунд.");
        }
    }
}
