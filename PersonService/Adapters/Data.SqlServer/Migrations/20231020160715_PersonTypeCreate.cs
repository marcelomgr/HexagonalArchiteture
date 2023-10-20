using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class PersonTypeCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdPersonType",
                table: "Persons");

            migrationBuilder.CreateTable(
                name: "PersonTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonTypes", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonTypes");

            migrationBuilder.AddColumn<int>(
                name: "IdPersonType",
                table: "Persons",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
