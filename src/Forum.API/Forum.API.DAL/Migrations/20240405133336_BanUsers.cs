using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Forum.API.DAL.Migrations
{
    /// <inheritdoc />
    public partial class BanUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BanTime",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BanType",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "BannedUser",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BanTime",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "BanType",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "BannedUser",
                table: "Users");
        }
    }
}
