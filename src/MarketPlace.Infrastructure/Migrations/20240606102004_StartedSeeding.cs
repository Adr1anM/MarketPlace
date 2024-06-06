using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketPlace.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class StartedSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorAuthorCategory_AuthorCategory_AuthorCategoryId",
                table: "AuthorAuthorCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AuthorCategory",
                table: "AuthorCategory");

            migrationBuilder.RenameTable(
                name: "AuthorCategory",
                newName: "AuthorCategories");

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "Products",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Authors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuthorCategories",
                table: "AuthorCategories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorAuthorCategory_AuthorCategories_AuthorCategoryId",
                table: "AuthorAuthorCategory",
                column: "AuthorCategoryId",
                principalTable: "AuthorCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorAuthorCategory_AuthorCategories_AuthorCategoryId",
                table: "AuthorAuthorCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AuthorCategories",
                table: "AuthorCategories");

            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Authors");

            migrationBuilder.RenameTable(
                name: "AuthorCategories",
                newName: "AuthorCategory");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuthorCategory",
                table: "AuthorCategory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorAuthorCategory_AuthorCategory_AuthorCategoryId",
                table: "AuthorAuthorCategory",
                column: "AuthorCategoryId",
                principalTable: "AuthorCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
