using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace codeKade.DataLayer.Migrations
{
    public partial class AddSoshialNetworksToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InstagramId",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TelegramId",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InstagramId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TelegramId",
                table: "Users");
        }
    }
}
