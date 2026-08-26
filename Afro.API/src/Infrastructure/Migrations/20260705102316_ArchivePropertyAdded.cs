using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Afro.API.src.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ArchivePropertyAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ArchivedAtUtc",
                table: "Owners",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                table: "Owners",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedUtc",
                table: "Owners",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArchivedAtUtc",
                table: "Owners");

            migrationBuilder.DropColumn(
                name: "IsArchived",
                table: "Owners");

            migrationBuilder.DropColumn(
                name: "UpdatedUtc",
                table: "Owners");
        }
    }
}
