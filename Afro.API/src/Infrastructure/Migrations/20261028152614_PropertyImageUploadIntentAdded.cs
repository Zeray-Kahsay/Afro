using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Afro.API.src.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PropertyImageUploadIntentAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PropertyImageUploadIntent",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PropertyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StorageProvider = table.Column<int>(type: "int", nullable: false),
                    StorageKey = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiresAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinalizedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyImageUploadIntent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyImageUploadIntent_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PropertyImageUploadIntent_PropertyId",
                table: "PropertyImageUploadIntent",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyImageUploadIntent_StorageProvider_StorageKey",
                table: "PropertyImageUploadIntent",
                columns: new[] { "StorageProvider", "StorageKey" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PropertyImageUploadIntent");
        }
    }
}
