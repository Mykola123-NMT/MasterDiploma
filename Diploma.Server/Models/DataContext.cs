using Microsoft.EntityFrameworkCore;

namespace Diploma.Server.Models
{
    public class DataContext: DbContext
    {
        public DataContext(): base() { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .SetBasePath(Directory.GetCurrentDirectory())
                .Build();
            optionsBuilder.UseSqlServer(config.GetConnectionString("sqlConnection"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne(p => p.ConsensusEvaluation)
                .WithOne(e => e.Product)
                .HasForeignKey<ConsensusEvaluation>(e => e.ProductId);
            modelBuilder.Entity<ExpertEvaluation>()
                .HasOne(e => e.Product)
                .WithMany(p => p.ExpertEvaluations)
                .HasForeignKey(e => e.ProductId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ExpertEvaluation>()
                .HasOne(eo => eo.Expert)
                .WithMany(e => e.ExpertEvaluations)
                .HasForeignKey(eo => eo.ExpertId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<LLMExpert> LLMExperts { get; set; }
        public DbSet<ExpertEvaluation> ExpertEvaluations { get; set; }
        public DbSet<ConsensusEvaluation> ConsensusEvaluations { get; set; }
    }
}
