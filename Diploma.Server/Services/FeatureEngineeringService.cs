using Diploma.Server.Interfaces;

namespace Diploma.Server.Services
{
    public class FeatureEngineeringService : IFeatureEngineeringService
    {
        private readonly IProductService _productService;
        private readonly IExpertEvaluationService _expertEvaluationService;
        private readonly IOpinionAgreementService _opinionAgreementService;

        public FeatureEngineeringService(IProductService productService, IExpertEvaluationService expertEvaluationService, IOpinionAgreementService opinionAgreementService)
        {
            _productService = productService;
            _expertEvaluationService = expertEvaluationService;
            _opinionAgreementService = opinionAgreementService;
        }

        public async Task EvaluateAllProductsAsync()
        {
            const int batchSize = 50; 
            var products = await _productService.GetProductsAsync();

            for (int i = 0; i < products.Count; i += batchSize)
            {
                var batch = products.Skip(i).Take(batchSize).ToList();

                var evaluationTasks = batch.Select(async product =>
                {
                    var expertEvaluations = await _expertEvaluationService.GetOpinionsAsync(product);
                    var consensusEvaluation = await _opinionAgreementService.GenerateConsensusOpinion(expertEvaluations);
                    product.ExpertEvaluations = expertEvaluations;
                    product.ConsensusEvaluation = consensusEvaluation;

                    await _productService.UpdateProductAsync(product.Asin, product);
                });

                await Task.WhenAll(evaluationTasks);
            }
        }
    }
}
