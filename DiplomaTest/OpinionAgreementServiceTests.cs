using Diploma.Server.Interfaces;
using Diploma.Server.Models;
using Diploma.Server.Services;
using Moq;

namespace DiplomaTest
{
    public class OpinionAgreementServiceTests
    {
        private OpinionAgreementService opinionAgreementService;
        private Mock<IExpertEvaluationService> _mockExpertEvaluationService;

        [SetUp]
        public void Setup()
        {
            opinionAgreementService = new OpinionAgreementService(Mock.Of<IExpertEvaluationService>());
            _mockExpertEvaluationService = new Mock<IExpertEvaluationService>();
            _mockExpertEvaluationService.Setup(s => s.GetOpinionsAsync(It.IsAny<Product>())).ReturnsAsync(new List<ExpertEvaluation>());
        }

        [Test]
        public void CalculateKendallCoefficient_ShouldReturnCorrectValue_ForValidArrays()
        {
            // Arrange
            double[] x = { 1, 2, 3, 4, 5 };
            double[] y = { 1, 2, 3, 4, 5 };

            // Act
            double result = opinionAgreementService.CalculateKendallCoefficient(x, y);

            // Assert
            Assert.That(result, Is.EqualTo(1.0), "Коефіцієнт Кендала для однакових масивів повинен бути 1.");
        }

        [Test]
        public void CalculateKendallCoefficient_ShouldThrowArgumentException_WhenArraysHaveDifferentLengths()
        {
            // Arrange
            var opinionAgreementService = new OpinionAgreementService(Mock.Of<IExpertEvaluationService>());

            double[] x = { 1, 2, 3 };
            double[] y = { 1, 2 };

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => opinionAgreementService.CalculateKendallCoefficient(x, y));
            Assert.That(exception.Message, Is.EqualTo("Масиви повинні бути однакової довжини"));
        }

        [Test]
        public void CalculateKendallCoefficient_ShouldHandleEdgeCases()
        {
            // Arrange
            var opinionAgreementService = new OpinionAgreementService(Mock.Of<IExpertEvaluationService>());

            // Крайні випадки
            double[] x = { 1, 1, 1, 1, 1 };  // Усі значення однакові
            double[] y = { 1, 1, 1, 1, 1 };  // Усі значення однакові

            // Act
            double result = opinionAgreementService.CalculateKendallCoefficient(x, y);

            // Assert
            Assert.That(result, Is.EqualTo(1.0), "Коефіцієнт Кендала для однакових значень повинен бути 1.");

            // Перевірка на нулі
            x = [0, 0, 0, 0];
            y = [0, 0, 0, 0];

            // Act
            result = opinionAgreementService.CalculateKendallCoefficient(x, y);

            // Assert
            Assert.That(result, Is.EqualTo(1.0), "Коефіцієнт Кендала для масивів з нулями має бути 1.");
        }

        [Test]
        public void CalculateKendallCoefficient_ShouldReturnExpectedResult_ForVariousDataSets()
        {
            // Arrange
            var opinionAgreementService = new OpinionAgreementService(Mock.Of<IExpertEvaluationService>());

            double[] x = { 1, 2, 3, 4, 5 };
            double[] y = { 5, 4, 3, 2, 1 };

            // Act
            double result = opinionAgreementService.CalculateKendallCoefficient(x, y);

            // Assert
            Assert.That(result, Is.EqualTo(-1.0), "Коефіцієнт Кендала для протилежних масивів має бути -1.");
        }

        [Test]
        public void CalculateAgreement_ThrowsArgumentException_WhenLessThanTwoExperts()
        {
            // Arrange
            var expertEvaluations = new List<ExpertEvaluation>
            {
                It.IsAny<ExpertEvaluation>() 
            };

            var opinionAgreementService = new OpinionAgreementService(Mock.Of<IExpertEvaluationService>());

            // Act & Assert
            Assert.Throws<ArgumentException>(() => opinionAgreementService.CalculateAgreement(expertEvaluations));
        }
    }
}
