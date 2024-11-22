using System.ComponentModel.DataAnnotations;

namespace Diploma.Server.Models
{
    public class LLMExpert
    {
        [Key]
        public int ExpertId {  get; set; }
        public required string Name { get; set; }
        public required string URL { get; set; }
        public ICollection<ExpertEvaluation>? ExpertEvaluations { get; set; }
    }
}
