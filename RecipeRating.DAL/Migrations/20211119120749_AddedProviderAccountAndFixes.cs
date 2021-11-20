using Microsoft.EntityFrameworkCore.Migrations;

namespace RecipeRating.DAL.Migrations
{
    public partial class AddedProviderAccountAndFixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Providers_ProviderId",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_ProviderId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "ProviderId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "ProviderUserId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "ProviderUserName",
                table: "Recipes");

            migrationBuilder.AddColumn<int>(
                name: "ProviderAccountId",
                table: "Recipes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Dishes",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Categories",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "ProviderAccount",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    ProviderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProviderAccount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProviderAccount_Providers_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Providers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_ProviderAccountId",
                table: "Recipes",
                column: "ProviderAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_ProviderAccount_ProviderId",
                table: "ProviderAccount",
                column: "ProviderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_ProviderAccount_ProviderAccountId",
                table: "Recipes",
                column: "ProviderAccountId",
                principalTable: "ProviderAccount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_ProviderAccount_ProviderAccountId",
                table: "Recipes");

            migrationBuilder.DropTable(
                name: "ProviderAccount");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_ProviderAccountId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "ProviderAccountId",
                table: "Recipes");

            migrationBuilder.AddColumn<int>(
                name: "ProviderId",
                table: "Recipes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ProviderUserId",
                table: "Recipes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProviderUserName",
                table: "Recipes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ImageUrl",
                table: "Dishes",
                type: "int",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "ImageUrl",
                table: "Categories",
                type: "int",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_ProviderId",
                table: "Recipes",
                column: "ProviderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Providers_ProviderId",
                table: "Recipes",
                column: "ProviderId",
                principalTable: "Providers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
