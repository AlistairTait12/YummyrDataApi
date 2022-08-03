using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YummyrDataApi.Migrations
{
    public partial class AddIngredientIdToDietaryInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IngredientId",
                table: "DietaryInfo",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IngredientId",
                table: "DietaryInfo");
        }
    }
}
