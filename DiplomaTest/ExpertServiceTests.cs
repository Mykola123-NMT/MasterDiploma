using Diploma.Server.Interfaces;
using Diploma.Server.Models;
using Diploma.Server.Services;
using Moq;

namespace DiplomaTest
{
    internal class ExpertServiceTests
    {
        [Test]
        public async Task GetExpertsAsync_ReturnsListOfExperts()
        {
            // Arrange
            var expectedExperts = new List<LLMExpert>
            {
                new LLMExpert { ExpertId = 1, URL = "", Name = "Expert 1" },
                new LLMExpert { ExpertId = 2, URL = "", Name = "Expert 2" }
            };

            var mockRepository = new Mock<IExpertRepository>();
            mockRepository.Setup(repo => repo.GetExpertsAsync()).ReturnsAsync(expectedExperts);

            var expertService = new ExpertService(mockRepository.Object);

            // Act
            var experts = await expertService.GetExpertsAsync();

            // Assert
            Assert.IsNotNull(experts);
            Assert.That(experts.Count, Is.EqualTo(2));
            Assert.That(experts[0].Name, Is.EqualTo("Expert 1"));
            Assert.That(experts[1].Name, Is.EqualTo("Expert 2"));
        }
        [Test]
        public async Task GetExpertsAsync_ReturnsEmptyList_WhenNoExpertsFound()
        {
            // Arrange
            var mockRepository = new Mock<IExpertRepository>();
            mockRepository.Setup(repo => repo.GetExpertsAsync()).ReturnsAsync(new List<LLMExpert>());

            var expertService = new ExpertService(mockRepository.Object);

            // Act
            var experts = await expertService.GetExpertsAsync();

            // Assert
            Assert.IsNotNull(experts);
            Assert.That(experts.Count, Is.EqualTo(0));
        }
        [Test]
        public async Task GetExpertsAsync_CallsRepositoryOnce()
        {
            // Arrange
            var mockRepository = new Mock<IExpertRepository>();
            mockRepository.Setup(repo => repo.GetExpertsAsync()).ReturnsAsync(new List<LLMExpert>());

            var expertService = new ExpertService(mockRepository.Object);

            // Act
            await expertService.GetExpertsAsync();

            // Assert
            mockRepository.Verify(repo => repo.GetExpertsAsync(), Times.Once);
        }

    }
}
