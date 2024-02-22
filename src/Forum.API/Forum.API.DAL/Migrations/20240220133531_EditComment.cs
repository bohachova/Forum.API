using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Forum.API.DAL.Migrations
{
    /// <inheritdoc />
    public partial class EditComment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastEdited",
                table: "Comments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "WasEdited",
                table: "Comments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastEdited",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "WasEdited",
                table: "Comments");
        }
    }
}
