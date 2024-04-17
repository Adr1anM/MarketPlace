using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketPlace.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedOnDeleteSetNULL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorAuthorCategory_AuthorCategory_AuthorCategoryId",
                table: "AuthorAuthorCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthorAuthorCategory_Authors_AuthorId",
                table: "AuthorAuthorCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_BuyerId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_BuyerId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "BuyerId",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "AuthorId",
                table: "AuthorAuthorCategory",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AuthorCategoryId",
                table: "AuthorAuthorCategory",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CretedById",
                table: "Orders",
                column: "CretedById");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorAuthorCategory_AuthorCategory_AuthorCategoryId",
                table: "AuthorAuthorCategory",
                column: "AuthorCategoryId",
                principalTable: "AuthorCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorAuthorCategory_Authors_AuthorId",
                table: "AuthorAuthorCategory",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_CretedById",
                table: "Orders",
                column: "CretedById",
                principalSchema: "Auth",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorAuthorCategory_AuthorCategory_AuthorCategoryId",
                table: "AuthorAuthorCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthorAuthorCategory_Authors_AuthorId",
                table: "AuthorAuthorCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_CretedById",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CretedById",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "BuyerId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "AuthorId",
                table: "AuthorAuthorCategory",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AuthorCategoryId",
                table: "AuthorAuthorCategory",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_BuyerId",
                table: "Orders",
                column: "BuyerId");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorAuthorCategory_AuthorCategory_AuthorCategoryId",
                table: "AuthorAuthorCategory",
                column: "AuthorCategoryId",
                principalTable: "AuthorCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorAuthorCategory_Authors_AuthorId",
                table: "AuthorAuthorCategory",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_BuyerId",
                table: "Orders",
                column: "BuyerId",
                principalSchema: "Auth",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
