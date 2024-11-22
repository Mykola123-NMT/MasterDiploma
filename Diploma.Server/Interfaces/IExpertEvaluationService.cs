using Diploma.Server.Models;

namespace Diploma.Server.Interfaces
{
    public interface IExpertEvaluationService
    {
        Task<List<ExpertEvaluation>> GetOpinionsAsync(Product product);
    }
}
