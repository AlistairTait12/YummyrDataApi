using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YummyrDataApi.Migrations
{
    public partial class UpdateDietaryInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DietaryType",
                table: "DietaryInfo",
                newName: "DietaryValue");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DietaryValue",
                table: "DietaryInfo",
                newName: "DietaryType");
        }
    }
}
