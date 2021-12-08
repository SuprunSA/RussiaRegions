using Microsoft.EntityFrameworkCore.Migrations;

namespace SubjectsAndDistrictsDbContext.Migrations
{
    public partial class PropertyId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subject_District_district_code",
                table: "Subject");

            migrationBuilder.RenameColumn(
                name: "district_code",
                table: "Subject",
                newName: "DistrictId");

            migrationBuilder.RenameIndex(
                name: "IX_Subject_district_code",
                table: "Subject",
                newName: "IX_Subject_DistrictId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subject_District_DistrictId",
                table: "Subject",
                column: "DistrictId",
                principalTable: "District",
                principalColumn: "code",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subject_District_DistrictId",
                table: "Subject");

            migrationBuilder.RenameColumn(
                name: "DistrictId",
                table: "Subject",
                newName: "district_code");

            migrationBuilder.RenameIndex(
                name: "IX_Subject_DistrictId",
                table: "Subject",
                newName: "IX_Subject_district_code");

            migrationBuilder.AddForeignKey(
                name: "FK_Subject_District_district_code",
                table: "Subject",
                column: "district_code",
                principalTable: "District",
                principalColumn: "code",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
