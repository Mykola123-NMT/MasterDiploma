namespace Diploma.Server.Models
{
    public class ConsensusEvaluation
    {
        public int ConsensusEvaluationId { get; set; }
        public double PriceStrategy { get; set; }
        public double Demand { get; set; }
        public double Quality { get; set; }
        public double PriceQuality { get; set; }
        public DateTime? Timestamp { get; set; }
        public required string ProductId { get; set; }
        public required Product Product { get; set; }
    }
}
