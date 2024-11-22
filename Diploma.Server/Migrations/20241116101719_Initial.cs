using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Diploma.Server.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LLMExperts",
                columns: table => new
                {
                    ExpertId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    URL = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LLMExperts", x => x.ExpertId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Asin = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImgUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stars = table.Column<double>(type: "float", nullable: true),
                    Reviews = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false),
                    ListPrice = table.Column<double>(type: "float", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    IsBestSeller = table.Column<bool>(type: "bit", nullable: true),
                    BoughtLastMonth = table.Column<double>(type: "float", nullable: true),
                    Discount = table.Column<double>(type: "float", nullable: true),
                    LogReviews = table.Column<double>(type: "float", nullable: true),
                    LogPrice = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Asin);
                });

            migrationBuilder.CreateTable(
                name: "ConsensusEvaluations",
                columns: table => new
                {
                    ConsensusEvaluationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PriceStrategy = table.Column<double>(type: "float", nullable: false),
                    Demand = table.Column<double>(type: "float", nullable: false),
                    Quality = table.Column<double>(type: "float", nullable: false),
                    PriceQuality = table.Column<double>(type: "float", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProductId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsensusEvaluations", x => x.ConsensusEvaluationId);
                    table.ForeignKey(
                        name: "FK_ConsensusEvaluations_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Asin",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExpertEvaluations",
                columns: table => new
                {
                    EvaluationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ExpertId = table.Column<int>(type: "int", nullable: false),
                    PriceStrategy = table.Column<double>(type: "float", nullable: false),
                    Demand = table.Column<double>(type: "float", nullable: false),
                    Quality = table.Column<double>(type: "float", nullable: false),
                    PriceQuality = table.Column<double>(type: "float", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Advantages = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Disadvantages = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpertEvaluations", x => x.EvaluationId);
                    table.ForeignKey(
                        name: "FK_ExpertEvaluations_LLMExperts_ExpertId",
                        column: x => x.ExpertId,
                        principalTable: "LLMExperts",
                        principalColumn: "ExpertId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExpertEvaluations_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Asin",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConsensusEvaluations_ProductId",
                table: "ConsensusEvaluations",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExpertEvaluations_ExpertId",
                table: "ExpertEvaluations",
                column: "ExpertId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpertEvaluations_ProductId",
                table: "ExpertEvaluations",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConsensusEvaluations");

            migrationBuilder.DropTable(
                name: "ExpertEvaluations");

            migrationBuilder.DropTable(
                name: "LLMExperts");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
