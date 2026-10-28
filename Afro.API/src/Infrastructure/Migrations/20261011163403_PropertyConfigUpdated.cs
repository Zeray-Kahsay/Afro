using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Afro.API.src.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PropertyConfigUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Properties_CreatedAtUtc_Id",
                table: "Properties",
                columns: new[] { "CreatedAtUtc", "Id" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Properties_CreatedAtUtc_Id",
                table: "Properties");
        }
    }
}
