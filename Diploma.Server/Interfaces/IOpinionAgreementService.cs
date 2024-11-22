using Diploma.Server.Models;

namespace Diploma.Server.Interfaces
{
    public interface IOpinionAgreementService
    {
        Task<ConsensusEvaluation> GenerateConsensusOpinion(List<ExpertEvaluation> evaluationResponses);
        double CalculateAgreement(List<ExpertEvaluation> evaluationResponses);
        double CalculateKendallCoefficient(double[] x, double[] y);
        double[][] ExtractExpertRatings(List<ExpertEvaluation> evaluationResponses);
    }
}
