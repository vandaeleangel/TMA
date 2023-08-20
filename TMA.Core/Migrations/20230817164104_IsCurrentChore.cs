using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMA.Core.Migrations
{
    /// <inheritdoc />
    public partial class IsCurrentChore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCurrentChore",
                table: "Chores",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCurrentChore",
                table: "Chores");
        }
    }
}
