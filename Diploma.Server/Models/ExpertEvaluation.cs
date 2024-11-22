using System.ComponentModel.DataAnnotations;

namespace Diploma.Server.Models
{
    public class ExpertEvaluation
    {
        [Key]
        public int EvaluationId { get; set; }
        public required string ProductId { get; set; }
        public required Product Product { get; set; }
        public required int ExpertId { get; set; }
        public required LLMExpert Expert {  get; set; }
        public double PriceStrategy { get; set; }
        public double Demand { get; set; }
        public double Quality { get; set; }
        public double PriceQuality { get; set; }
        public DateTime Timestamp { get; set; }
        public string[]? Advantages { get; set; }
        public string[]? Disadvantages { get; set; }
    }
}
