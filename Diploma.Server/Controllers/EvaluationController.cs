using Microsoft.AspNetCore.Mvc;
using Diploma.Server.Models;
using Diploma.Server.Interfaces;
using static Diploma_Server.RegressionModel;

namespace Diploma.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EvaluationController : ControllerBase
    {
        private readonly IExpertEvaluationService _evaluationService;
        private readonly IOpinionAgreementService _opinionAgreementService;
        private readonly IProductService _productService;
        public EvaluationController(IExpertEvaluationService evaluationService, IOpinionAgreementService opinionAgreementService, IProductService ProductService)
        {
            _evaluationService = evaluationService;
            _opinionAgreementService = opinionAgreementService;
            _productService = ProductService;
        }

        // POST: api/Products/ExpertOpinions
        [HttpPost("ExpertOpinions")]
        public async Task<ActionResult<IEnumerable<ExpertEvaluation>>> GetExpertOpinions([FromBody]Product product)
        {
            var opinions = await _evaluationService.GetOpinionsAsync(product);
            return Ok(opinions);
        }

        // POST: api/Products/ConsensusOpinion
        [HttpPost("ConsensusOpinion")]
        public ActionResult<ConsensusEvaluation> ConsensusOpinion([FromBody]List<ExpertEvaluation> expertOpinions)
        {
            var consensusOpinion = _opinionAgreementService.GenerateConsensusOpinion(expertOpinions);
            return Ok(consensusOpinion);
        }

        // POST: api/Products/RegressionResult
        [HttpPost("RegressionResult")]
        public ActionResult<Product> GetRegressionResult([FromBody]ConsensusEvaluation consensusOpinion)
        {
            var modelInput = _productService.GetRegressionModelInput(consensusOpinion);
            var modelOutput = Predict(modelInput);
            return Ok(modelOutput);
        }
    }
}
