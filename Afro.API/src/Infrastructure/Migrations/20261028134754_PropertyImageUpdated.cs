using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Afro.API.src.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PropertyImageUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "StorageProvider",
                table: "PropertyImages",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "StorageKey",
                table: "PropertyImages",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyImages_PropertyId_SortOrder",
                table: "PropertyImages",
                columns: new[] { "PropertyId", "SortOrder" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PropertyImages_PropertyId_SortOrder",
                table: "PropertyImages");

            migrationBuilder.DropColumn(
                name: "StorageKey",
                table: "PropertyImages");

            migrationBuilder.AlterColumn<string>(
                name: "StorageProvider",
                table: "PropertyImages",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
