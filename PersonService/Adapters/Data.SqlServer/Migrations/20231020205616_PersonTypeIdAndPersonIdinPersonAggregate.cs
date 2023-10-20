using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class PersonTypeIdAndPersonIdinPersonAggregate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonAggregates_Persons_PersonId",
                table: "PersonAggregates");

            migrationBuilder.AlterColumn<int>(
                name: "PersonId",
                table: "PersonAggregates",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonAggregates_Persons_PersonId",
                table: "PersonAggregates",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonAggregates_Persons_PersonId",
                table: "PersonAggregates");

            migrationBuilder.AlterColumn<int>(
                name: "PersonId",
                table: "PersonAggregates",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonAggregates_Persons_PersonId",
                table: "PersonAggregates",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id");
        }
    }
}
