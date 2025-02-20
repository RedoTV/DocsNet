using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocsNetAPI.Migrations
{
    /// <inheritdoc />
    public partial class Changedocumenttablescheme : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TransferDate",
                table: "DocumentHistory",
                newName: "ExpirationDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpirationDate",
                table: "Documents",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpirationDate",
                table: "Documents");

            migrationBuilder.RenameColumn(
                name: "ExpirationDate",
                table: "DocumentHistory",
                newName: "TransferDate");
        }
    }
}
