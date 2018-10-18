using Microsoft.EntityFrameworkCore.Migrations;

namespace pindogramApp.Migrations
{
    public partial class DownUpVoteFlagForMeme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CanDownVote",
                table: "Memes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanUpVote",
                table: "Memes",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanDownVote",
                table: "Memes");

            migrationBuilder.DropColumn(
                name: "CanUpVote",
                table: "Memes");
        }
    }
}
