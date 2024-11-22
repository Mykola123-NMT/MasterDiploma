using Diploma.Server.Models;

namespace Diploma.Server.Interfaces
{
    public interface IExpertService
    {
        Task<List<LLMExpert>> GetExpertsAsync();
    }
}
