using Microsoft.EntityFrameworkCore.Migrations;

namespace MyDataAccessLayer.Migrations
{
    public partial class AddPenBrandMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pens",
                columns: table => new
                {
                    PenId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pens", x => x.PenId);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PenBrands");

            migrationBuilder.DropTable(
                name: "Pens");
        }
    }
}
