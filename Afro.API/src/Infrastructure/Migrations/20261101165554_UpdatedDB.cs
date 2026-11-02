using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Afro.API.src.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyImageUploadIntent_Properties_PropertyId",
                table: "PropertyImageUploadIntent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PropertyImageUploadIntent",
                table: "PropertyImageUploadIntent");

            migrationBuilder.RenameTable(
                name: "PropertyImageUploadIntent",
                newName: "PropertyImageUploadIntents");

            migrationBuilder.RenameIndex(
                name: "IX_PropertyImageUploadIntent_StorageProvider_StorageKey",
                table: "PropertyImageUploadIntents",
                newName: "IX_PropertyImageUploadIntents_StorageProvider_StorageKey");

            migrationBuilder.RenameIndex(
                name: "IX_PropertyImageUploadIntent_PropertyId",
                table: "PropertyImageUploadIntents",
                newName: "IX_PropertyImageUploadIntents_PropertyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PropertyImageUploadIntents",
                table: "PropertyImageUploadIntents",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyImageUploadIntents_Properties_PropertyId",
                table: "PropertyImageUploadIntents",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyImageUploadIntents_Properties_PropertyId",
                table: "PropertyImageUploadIntents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PropertyImageUploadIntents",
                table: "PropertyImageUploadIntents");

            migrationBuilder.RenameTable(
                name: "PropertyImageUploadIntents",
                newName: "PropertyImageUploadIntent");

            migrationBuilder.RenameIndex(
                name: "IX_PropertyImageUploadIntents_StorageProvider_StorageKey",
                table: "PropertyImageUploadIntent",
                newName: "IX_PropertyImageUploadIntent_StorageProvider_StorageKey");

            migrationBuilder.RenameIndex(
                name: "IX_PropertyImageUploadIntents_PropertyId",
                table: "PropertyImageUploadIntent",
                newName: "IX_PropertyImageUploadIntent_PropertyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PropertyImageUploadIntent",
                table: "PropertyImageUploadIntent",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyImageUploadIntent_Properties_PropertyId",
                table: "PropertyImageUploadIntent",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
