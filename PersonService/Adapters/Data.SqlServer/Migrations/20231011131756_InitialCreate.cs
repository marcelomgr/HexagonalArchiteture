using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.SqlServer.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MotherName = table.Column<int>(type: "int", nullable: true),
                    Rg = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cpf = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CondemnedRegister = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CondemnationArticle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CondemnationProccess = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CondemnationCourt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CondemnationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdPersonType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Persons");
        }
    }
}
