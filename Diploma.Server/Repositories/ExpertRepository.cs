using Diploma.Server.Interfaces;
using Diploma.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Diploma.Server.Repositories
{
    public class ExpertRepository : IExpertRepository
    {
        private readonly DataContext _context;
        public ExpertRepository(DataContext context) 
        {
            _context = context;
        }
        public async Task<List<LLMExpert>> GetExpertsAsync()
        {
            var experts = await _context.LLMExperts.ToListAsync();
            return experts;
        }
    }
}
