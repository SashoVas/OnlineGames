using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineGames.Data.Migrations
{
    public partial class FirstPlayerName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstPlayerName",
                table: "TicTacToeRooms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstPlayerName",
                table: "TicTacToeRooms");
        }
    }
}
