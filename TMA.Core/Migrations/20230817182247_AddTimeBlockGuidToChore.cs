using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMA.Core.Migrations
{
    /// <inheritdoc />
    public partial class AddTimeBlockGuidToChore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CurrentTimeBlockId",
                table: "Chores",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentTimeBlockId",
                table: "Chores");
        }
    }
}
