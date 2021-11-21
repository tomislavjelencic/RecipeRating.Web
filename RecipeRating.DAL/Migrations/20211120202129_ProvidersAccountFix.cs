using Microsoft.EntityFrameworkCore.Migrations;

namespace RecipeRating.DAL.Migrations
{
    public partial class ProvidersAccountFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProviderAccount_Providers_ProviderId",
                table: "ProviderAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_ProviderAccount_ProviderAccountId",
                table: "Recipes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProviderAccount",
                table: "ProviderAccount");

            migrationBuilder.RenameTable(
                name: "ProviderAccount",
                newName: "ProviderAccounts");

            migrationBuilder.RenameIndex(
                name: "IX_ProviderAccount_ProviderId",
                table: "ProviderAccounts",
                newName: "IX_ProviderAccounts_ProviderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProviderAccounts",
                table: "ProviderAccounts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProviderAccounts_Providers_ProviderId",
                table: "ProviderAccounts",
                column: "ProviderId",
                principalTable: "Providers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_ProviderAccounts_ProviderAccountId",
                table: "Recipes",
                column: "ProviderAccountId",
                principalTable: "ProviderAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProviderAccounts_Providers_ProviderId",
                table: "ProviderAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_ProviderAccounts_ProviderAccountId",
                table: "Recipes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProviderAccounts",
                table: "ProviderAccounts");

            migrationBuilder.RenameTable(
                name: "ProviderAccounts",
                newName: "ProviderAccount");

            migrationBuilder.RenameIndex(
                name: "IX_ProviderAccounts_ProviderId",
                table: "ProviderAccount",
                newName: "IX_ProviderAccount_ProviderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProviderAccount",
                table: "ProviderAccount",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProviderAccount_Providers_ProviderId",
                table: "ProviderAccount",
                column: "ProviderId",
                principalTable: "Providers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_ProviderAccount_ProviderAccountId",
                table: "Recipes",
                column: "ProviderAccountId",
                principalTable: "ProviderAccount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
