using Diploma.Server.Interfaces;
using Diploma.Server.Models;

namespace Diploma.Server.Services
{
    public class ExpertService : IExpertService
    {
        private readonly IExpertRepository _repository;
        public ExpertService(IExpertRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<LLMExpert>> GetExpertsAsync()
        {
            return await _repository.GetExpertsAsync();
        }
    }
}
