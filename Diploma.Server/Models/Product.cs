using System.ComponentModel.DataAnnotations;

namespace Diploma.Server.Models
{
    public class Product
    {
        [Key]
        public string Asin { get; set; }
        public string Title { get; set; }
        public string? ImgUrl { get; set; }
        public string? ProductUrl { get; set; }
        public double? Stars { get; set; }
        public int? Reviews { get; set; }
        public double Price { get; set; }
        public double? ListPrice { get; set; }
        public int? CategoryId { get; set; }
        public bool? IsBestSeller { get; set; }
        public double? BoughtLastMonth { get; set; }
        public double? Discount { get; set; }
        public double? LogReviews { get; set; }
        public double? LogPrice { get; set; }
        public ICollection<ExpertEvaluation>? ExpertEvaluations { get; set; }
        public ConsensusEvaluation? ConsensusEvaluation { get; set; }

        public Product() 
        {
            Asin = "";
            Title = "";
        }
        public Product(Product other)
        {
            Asin = other.Asin;
            Title = other.Title;
            ImgUrl = other.ImgUrl;
            ProductUrl = other.ProductUrl;
            Stars = other.Stars;
            Reviews = other.Reviews;
            Price = other.Price;
            ListPrice = other.ListPrice;
            CategoryId = other.CategoryId;
            IsBestSeller = other.IsBestSeller;
            BoughtLastMonth = other.BoughtLastMonth;
            Discount = other.Discount;
            LogReviews = other.LogReviews;
            LogPrice = other.LogPrice;
        }
    }
}
