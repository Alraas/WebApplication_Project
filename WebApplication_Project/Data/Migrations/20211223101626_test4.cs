using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication_Project.Data.Migrations
{
    public partial class test4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Categorie_CategorieID",
                table: "Product");

            migrationBuilder.AlterColumn<int>(
                name: "CategorieID",
                table: "Product",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Categorie_CategorieID",
                table: "Product",
                column: "CategorieID",
                principalTable: "Categorie",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Categorie_CategorieID",
                table: "Product");

            migrationBuilder.AlterColumn<int>(
                name: "CategorieID",
                table: "Product",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Categorie_CategorieID",
                table: "Product",
                column: "CategorieID",
                principalTable: "Categorie",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
