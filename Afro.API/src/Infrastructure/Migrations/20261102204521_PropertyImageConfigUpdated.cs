using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Afro.API.src.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PropertyImageConfigUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PropertyImages_PropertyId_StorageProvider_StorageKey",
                table: "PropertyImages",
                columns: new[] { "PropertyId", "StorageProvider", "StorageKey" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PropertyImages_PropertyId_StorageProvider_StorageKey",
                table: "PropertyImages");
        }
    }
}
