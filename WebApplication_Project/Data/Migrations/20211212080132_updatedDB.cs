using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication_Project.Data.Migrations
{
    public partial class updatedDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_Customer_CustomerID",
                table: "Cart");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Categories_CategorieID",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Cart_CustomerID",
                table: "Cart");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Order_details");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Order_details");

            migrationBuilder.DropColumn(
                name: "CustomerID",
                table: "Cart");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Product",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CategorieID",
                table: "Product",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Order",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Order",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Cart_Customer_ID",
                table: "Cart",
                column: "Customer_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_Customer_Customer_ID",
                table: "Cart",
                column: "Customer_ID",
                principalTable: "Customer",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Categories_CategorieID",
                table: "Product",
                column: "CategorieID",
                principalTable: "Categories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_Customer_Customer_ID",
                table: "Cart");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Categories_CategorieID",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Cart_Customer_ID",
                table: "Cart");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Order");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Product",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "CategorieID",
                table: "Product",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Order_details",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Order_details",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CustomerID",
                table: "Cart",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cart_CustomerID",
                table: "Cart",
                column: "CustomerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_Customer_CustomerID",
                table: "Cart",
                column: "CustomerID",
                principalTable: "Customer",
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
    }
}
