using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YummyrDataApi.Migrations
{
    public partial class ConvertDietaryTypeToStringInDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DietaryType",
                table: "DietaryInfo",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "DietaryType",
                table: "DietaryInfo",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
