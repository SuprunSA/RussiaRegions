using Microsoft.EntityFrameworkCore.Migrations;

namespace SubjectsAndDistrictsDbContext.Migrations
{
    public partial class TableNamesInAPI : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subject_Dstrict_district_code",
                table: "Subject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dstrict",
                table: "Dstrict");

            migrationBuilder.RenameTable(
                name: "Dstrict",
                newName: "District");

            migrationBuilder.AddPrimaryKey(
                name: "PK_District",
                table: "District",
                column: "code");

            migrationBuilder.AddForeignKey(
                name: "FK_Subject_District_district_code",
                table: "Subject",
                column: "district_code",
                principalTable: "District",
                principalColumn: "code",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subject_District_district_code",
                table: "Subject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_District",
                table: "District");

            migrationBuilder.RenameTable(
                name: "District",
                newName: "Dstrict");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dstrict",
                table: "Dstrict",
                column: "code");

            migrationBuilder.AddForeignKey(
                name: "FK_Subject_Dstrict_district_code",
                table: "Subject",
                column: "district_code",
                principalTable: "Dstrict",
                principalColumn: "code",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
