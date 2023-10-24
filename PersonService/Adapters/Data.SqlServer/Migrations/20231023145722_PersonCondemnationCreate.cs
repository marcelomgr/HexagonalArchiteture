using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class PersonCondemnationCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CondemnationArticle",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "CondemnationCourt",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "CondemnationDate",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "CondemnationProccess",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "CondemnedRegister",
                table: "Persons");

            migrationBuilder.AddColumn<int>(
                name: "PersonCondemnationId",
                table: "PersonAggregates",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PersonCondemnation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CondemnedRegister = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CondemnationArticle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CondemnationProccess = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CondemnationCourt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CondemnationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    PersonAggregateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonCondemnation", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonAggregates_PersonCondemnationId",
                table: "PersonAggregates",
                column: "PersonCondemnationId",
                unique: true,
                filter: "[PersonCondemnationId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonAggregates_PersonCondemnation_PersonCondemnationId",
                table: "PersonAggregates",
                column: "PersonCondemnationId",
                principalTable: "PersonCondemnation",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonAggregates_PersonCondemnation_PersonCondemnationId",
                table: "PersonAggregates");

            migrationBuilder.DropTable(
                name: "PersonCondemnation");

            migrationBuilder.DropIndex(
                name: "IX_PersonAggregates_PersonCondemnationId",
                table: "PersonAggregates");

            migrationBuilder.DropColumn(
                name: "PersonCondemnationId",
                table: "PersonAggregates");

            migrationBuilder.AddColumn<string>(
                name: "CondemnationArticle",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CondemnationCourt",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CondemnationDate",
                table: "Persons",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CondemnationProccess",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CondemnedRegister",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
