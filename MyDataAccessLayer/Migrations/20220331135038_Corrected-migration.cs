using Microsoft.EntityFrameworkCore.Migrations;

namespace MyDataAccessLayer.Migrations
{
    public partial class Correctedmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pens_Brands_BrandId",
                table: "Pens");

            migrationBuilder.DropColumn(
                name: "PenId",
                table: "Brands");

            migrationBuilder.AlterColumn<int>(
                name: "BrandId",
                table: "Pens",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pens_Brands_BrandId",
                table: "Pens",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pens_Brands_BrandId",
                table: "Pens");

            migrationBuilder.AlterColumn<int>(
                name: "BrandId",
                table: "Pens",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "PenId",
                table: "Brands",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Pens_Brands_BrandId",
                table: "Pens",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
