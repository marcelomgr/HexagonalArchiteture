using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class AddPersonGender : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Persons");

            migrationBuilder.AddColumn<int>(
                name: "PersonGenderId",
                table: "Persons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PersonGenders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonGenders", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Persons_PersonGenderId",
                table: "Persons",
                column: "PersonGenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_PersonGenders_PersonGenderId",
                table: "Persons",
                column: "PersonGenderId",
                principalTable: "PersonGenders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_PersonGenders_PersonGenderId",
                table: "Persons");

            migrationBuilder.DropTable(
                name: "PersonGenders");

            migrationBuilder.DropIndex(
                name: "IX_Persons_PersonGenderId",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "PersonGenderId",
                table: "Persons");

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
