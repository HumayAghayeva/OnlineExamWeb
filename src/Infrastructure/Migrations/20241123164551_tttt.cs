using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class tttt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentPhotos_Students_StudentIdID",
                table: "StudentPhotos");

            migrationBuilder.RenameColumn(
                name: "Url",
                table: "StudentPhotos",
                newName: "PhotoPath");

            migrationBuilder.RenameColumn(
                name: "StudentIdID",
                table: "StudentPhotos",
                newName: "StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentPhotos_StudentIdID",
                table: "StudentPhotos",
                newName: "IX_StudentPhotos_StudentId");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "StudentPhotos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentPhotos_Students_StudentId",
                table: "StudentPhotos",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentPhotos_Students_StudentId",
                table: "StudentPhotos");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "StudentPhotos");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "StudentPhotos",
                newName: "StudentIdID");

            migrationBuilder.RenameColumn(
                name: "PhotoPath",
                table: "StudentPhotos",
                newName: "Url");

            migrationBuilder.RenameIndex(
                name: "IX_StudentPhotos_StudentId",
                table: "StudentPhotos",
                newName: "IX_StudentPhotos_StudentIdID");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentPhotos_Students_StudentIdID",
                table: "StudentPhotos",
                column: "StudentIdID",
                principalTable: "Students",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
