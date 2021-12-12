using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication_Project.Data.Migrations
{
    public partial class updatedDB3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_details_Product_ProductID",
                table: "Order_details");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Categories_CategorieID",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Order_details_ProductID",
                table: "Order_details");

            migrationBuilder.DropColumn(
                name: "ProductID",
                table: "Order_details");

            migrationBuilder.AlterColumn<int>(
                name: "CategorieID",
                table: "Product",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CategorieID",
                table: "Order_details",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Order_details_CategorieID",
                table: "Order_details",
                column: "CategorieID");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_details_Product_CategorieID",
                table: "Order_details",
                column: "CategorieID",
                principalTable: "Product",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Categories_CategorieID",
                table: "Product",
                column: "CategorieID",
                principalTable: "Categories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_details_Product_CategorieID",
                table: "Order_details");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Categories_CategorieID",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Order_details_CategorieID",
                table: "Order_details");

            migrationBuilder.DropColumn(
                name: "CategorieID",
                table: "Order_details");

            migrationBuilder.AlterColumn<int>(
                name: "CategorieID",
                table: "Product",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductID",
                table: "Order_details",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Order_details_ProductID",
                table: "Order_details",
                column: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_details_Product_ProductID",
                table: "Order_details",
                column: "ProductID",
                principalTable: "Product",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Categories_CategorieID",
                table: "Product",
                column: "CategorieID",
                principalTable: "Categories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
