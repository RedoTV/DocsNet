using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocsNetAPI.Migrations
{
    /// <inheritdoc />
    public partial class changecommentsdbrelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentComment_Documents_DocumentId",
                table: "DocumentComment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DocumentComment",
                table: "DocumentComment");

            migrationBuilder.RenameTable(
                name: "DocumentComment",
                newName: "DocumentComments");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentComment_DocumentId",
                table: "DocumentComments",
                newName: "IX_DocumentComments_DocumentId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "DocumentComments",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DocumentComments",
                table: "DocumentComments",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentComments_UserId",
                table: "DocumentComments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentComments_AspNetUsers_UserId",
                table: "DocumentComments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentComments_Documents_DocumentId",
                table: "DocumentComments",
                column: "DocumentId",
                principalTable: "Documents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentComments_AspNetUsers_UserId",
                table: "DocumentComments");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentComments_Documents_DocumentId",
                table: "DocumentComments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DocumentComments",
                table: "DocumentComments");

            migrationBuilder.DropIndex(
                name: "IX_DocumentComments_UserId",
                table: "DocumentComments");

            migrationBuilder.RenameTable(
                name: "DocumentComments",
                newName: "DocumentComment");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentComments_DocumentId",
                table: "DocumentComment",
                newName: "IX_DocumentComment_DocumentId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "DocumentComment",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DocumentComment",
                table: "DocumentComment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentComment_Documents_DocumentId",
                table: "DocumentComment",
                column: "DocumentId",
                principalTable: "Documents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
