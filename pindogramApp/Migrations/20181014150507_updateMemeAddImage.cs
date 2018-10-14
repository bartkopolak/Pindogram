using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace pindogramApp.Migrations
{
    public partial class updateMemeAddImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Memes",
                nullable: true);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Memes");
        }
    }
}
