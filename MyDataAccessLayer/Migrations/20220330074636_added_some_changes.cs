using Microsoft.EntityFrameworkCore.Migrations;

namespace MyDataAccessLayer.Migrations
{
    public partial class added_some_changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PenBrands");

            migrationBuilder.DropColumn(
                name: "Brand",
                table: "Pens");

            migrationBuilder.RenameColumn(
                name: "PenId",
                table: "Pens",
                newName: "Id");

            migrationBuilder.AddColumn<int>(
                name: "BrandId",
                table: "Pens",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PenId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pens_BrandId",
                table: "Pens",
                column: "BrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pens_Brands_BrandId",
                table: "Pens",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pens_Brands_BrandId",
                table: "Pens");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropIndex(
                name: "IX_Pens_BrandId",
                table: "Pens");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "Pens");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Pens",
                newName: "PenId");

            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "Pens",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PenBrands",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PenId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PenBrands", x => x.id);
                    table.ForeignKey(
                        name: "FK_PenBrands_Pens_PenId",
                        column: x => x.PenId,
                        principalTable: "Pens",
                        principalColumn: "PenId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PenBrands_PenId",
                table: "PenBrands",
                column: "PenId",
                unique: true);
        }
    }
}
