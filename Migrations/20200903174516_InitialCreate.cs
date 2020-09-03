using Microsoft.EntityFrameworkCore.Migrations;

namespace writeAJoke.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Joke",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Joke",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Joke");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Joke");
        }
    }
}
