using Diploma.Server.Models;
using NuGet.Protocol.Core.Types;

namespace Diploma.Server.Interfaces
{
    public interface IExpertRepository
    {
        Task<List<LLMExpert>> GetExpertsAsync();
    }
}
