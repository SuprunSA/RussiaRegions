using Microsoft.EntityFrameworkCore.Migrations;

namespace SubjectsAndDistrictsDbContext.Migrations
{
    public partial class Initialize_Db : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dstrict",
                columns: table => new
                {
                    code = table.Column<long>(type: "bigint", nullable: false),
                    name = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dstrict", x => x.code);
                });

            migrationBuilder.CreateTable(
                name: "Subject",
                columns: table => new
                {
                    code = table.Column<long>(type: "bigint", nullable: false),
                    name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    admin_center_name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    population = table.Column<double>(type: "float(53)", nullable: false),
                    square = table.Column<double>(type: "float(53)", nullable: false),
                    district_code = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subject", x => x.code);
                    table.ForeignKey(
                        name: "FK_Subject_Dstrict_district_code",
                        column: x => x.district_code,
                        principalTable: "Dstrict",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subject_district_code",
                table: "Subject",
                column: "district_code");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Subject");

            migrationBuilder.DropTable(
                name: "Dstrict");
        }
    }
}
