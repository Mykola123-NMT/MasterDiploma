using Diploma.Server.Interfaces;
using Diploma.Server.Models;

namespace Diploma.Server.Services
{
    public class OpinionAgreementService: IOpinionAgreementService
    {
        private Dictionary<(int, int), double> concordanceResults;
        private double[][] expertRatings;
        private readonly IExpertEvaluationService _expertEvaluationService;

        public OpinionAgreementService(IExpertEvaluationService expertEvaluationService)
        {
            concordanceResults = new Dictionary<(int, int), double>();
            expertRatings = Array.Empty<double[]>();
            _expertEvaluationService = expertEvaluationService;
        }
        public async Task<ConsensusEvaluation> GenerateConsensusOpinion(List<ExpertEvaluation> evaluationResponses)
        {
            int numExperts = evaluationResponses.Count;
            double overallConcordance = CalculateAgreement(evaluationResponses);
            string productId = evaluationResponses.First().ProductId;
            Product product = evaluationResponses.First().Product;

            if (overallConcordance > 0.7)
            {
                // Середнє значення для кожної характеристики
                return new ConsensusEvaluation
                {
                    ProductId = productId,
                    Product = product,
                    PriceStrategy = evaluationResponses.Average(e => e.PriceStrategy),
                    Demand = evaluationResponses.Average(e => e.Demand),
                    Quality = evaluationResponses.Average(e => e.Quality),
                    PriceQuality = evaluationResponses.Average(e => e.PriceQuality)
                };
            }
            else
            {
                // Знаходимо пару з найбільшим коефіцієнтом конкордації
                double maxConcordance = 0.0;
                (int, int) bestPair = (0, 1);

                for (int i = 0; i < numExperts; i++)
                {
                    for (int j = i + 1; j < numExperts; j++)
                    {
                        double concordance = concordanceResults[(i, j)];
                        if (concordance > maxConcordance)
                        {
                            maxConcordance = concordance;
                            bestPair = (i, j);
                        }
                    }
                }

                // Якщо найвищий коефіцієнт конкордації > 0.5, усереднюємо оцінки цієї пари
                if (maxConcordance > 0.5)
                {
                    return new ConsensusEvaluation
                    {
                        ProductId = productId,
                        Product = product,
                        PriceStrategy = (evaluationResponses[bestPair.Item1].PriceStrategy + evaluationResponses[bestPair.Item2].PriceStrategy) / 2,
                        Demand = (evaluationResponses[bestPair.Item1].Demand + evaluationResponses[bestPair.Item2].Demand) / 2,
                        Quality = (evaluationResponses[bestPair.Item1].Quality + evaluationResponses[bestPair.Item2].Quality) / 2,
                        PriceQuality = (evaluationResponses[bestPair.Item1].PriceQuality + evaluationResponses[bestPair.Item2].PriceQuality) / 2
                    };
                }
                else
                {
                    // Якщо жодна пара не має коефіцієнт конкордації > 0.5, проводимо повторну переоцінку
                    var reevaluatedResponses = await _expertEvaluationService.GetOpinionsAsync(product);
                    var consensusOpinion = await GenerateConsensusOpinion(reevaluatedResponses);
                    return consensusOpinion;
                }
            }
        }
        public double CalculateAgreement(List<ExpertEvaluation> evaluationResponses)
        {
            int numExperts = evaluationResponses.Count;

            // Перевірка на валідність
            if (numExperts < 2) 
            {
                throw new ArgumentException("Необхідно хоча б двоє експертів");
            }

            expertRatings = ExtractExpertRatings(evaluationResponses);

            // Обчислюємо середній коефіцієнт конкордації для всіх пар експертів
            double totalConcordance = 0;
            int pairCount = 0;

            // Порівнюємо кожну пару експертів
            for (int i = 0; i < numExperts; i++)
            {
                for (int j = i + 1; j < numExperts; j++)
                {
                   var concordance = CalculateKendallCoefficient(expertRatings[i], expertRatings[j]);
                    concordanceResults[(i, j)] = concordance;
                    totalConcordance += concordance;
                    pairCount++;
                }
            }

            // Повертаємо середній коефіцієнт конкордації
            return totalConcordance / pairCount;
        }

        public double CalculateKendallCoefficient(double[] x, double[] y)
        {
            if (x.Length != y.Length)
            {
                throw new ArgumentException("Масиви повинні бути однакової довжини");
            }

            int n = x.Length;

            // Перевірка на однакові значення в масивах
            if (x.All(val => val == x[0]) && y.All(val => val == y[0]))
            {
                return 1.0;  // Якщо всі значення однакові, коефіцієнт Кендала має бути 1
            }

            // Обчислення середніх значень
            double meanX = x.Average(), meanY = y.Average();

            // Обчислення коваріації
            double sxy = 0;
            for (int i = 0; i < n; i++)
            {
                sxy += (x[i] - meanX) * (y[i] - meanY);
            }
            sxy /= n;

            // Обчислення дисперсій
            double varX = x.Select(val => Math.Pow(val - meanX, 2)).Sum() / n;
            double varY = y.Select(val => Math.Pow(val - meanY, 2)).Sum() / n;

            // Обчислення коригованого коефіцієнта Кендала
            double rhoc = 2 * sxy / (varX + varY + Math.Pow(meanX - meanY, 2));

            return rhoc;
        }

        public double[][] ExtractExpertRatings(List<ExpertEvaluation> evaluationResponses)
        {
            return evaluationResponses.Select(response => new double[]
            {
                response.PriceStrategy,
                response.Demand,
                response.Quality,
                response.PriceQuality
            }).ToArray();
        }
    }
}
