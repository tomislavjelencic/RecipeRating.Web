using Microsoft.EntityFrameworkCore.Migrations;

namespace RecipeRating.DAL.Migrations
{
    public partial class YtThumbnailUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ThumbnailUrl",
                table: "Recipes",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThumbnailUrl",
                table: "Recipes");
        }
    }
}
