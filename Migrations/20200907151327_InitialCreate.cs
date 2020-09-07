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

            migrationBuilder.AddColumn<int>(
                name: "eyeRolls",
                table: "Joke",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "laughs",
                table: "Joke",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "smiles",
                table: "Joke",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Joke");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Joke");

            migrationBuilder.DropColumn(
                name: "eyeRolls",
                table: "Joke");

            migrationBuilder.DropColumn(
                name: "laughs",
                table: "Joke");

            migrationBuilder.DropColumn(
                name: "smiles",
                table: "Joke");
        }
    }
}
