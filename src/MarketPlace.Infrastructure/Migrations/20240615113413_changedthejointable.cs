using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketPlace.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changedthejointable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategSubCateg");

            migrationBuilder.CreateTable(
                name: "ProductSubCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    SubCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSubCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductSubCategories_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductSubCategories_SubCategories_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "SubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductSubCategories_ProductId",
                table: "ProductSubCategories",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSubCategories_SubCategoryId",
                table: "ProductSubCategories",
                column: "SubCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductSubCategories");

            migrationBuilder.CreateTable(
                name: "CategSubCateg",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    SubCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategSubCateg", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategSubCateg_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategSubCateg_SubCategories_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "SubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategSubCateg_CategoryId",
                table: "CategSubCateg",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CategSubCateg_SubCategoryId",
                table: "CategSubCateg",
                column: "SubCategoryId");
        }
    }
}
