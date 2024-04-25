using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Forum.API.DAL.Migrations
{
    /// <inheritdoc />
    public partial class DeleteUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DeletedUser",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedUser",
                table: "Users");
        }
    }
}
