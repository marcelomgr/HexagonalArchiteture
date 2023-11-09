using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class CreatedColumnInChangeLogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ChangeDateTime",
                table: "ChangeLogs",
                newName: "Created");

            migrationBuilder.AddColumn<int>(
                name: "PersonId",
                table: "ChangeLogs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChangeLogs_PersonId",
                table: "ChangeLogs",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeLogs_Persons_PersonId",
                table: "ChangeLogs",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChangeLogs_Persons_PersonId",
                table: "ChangeLogs");

            migrationBuilder.DropIndex(
                name: "IX_ChangeLogs_PersonId",
                table: "ChangeLogs");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "ChangeLogs");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "ChangeLogs",
                newName: "ChangeDateTime");
        }
    }
}
