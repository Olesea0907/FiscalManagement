using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FiscalManagement.Migrations
{
    /// <inheritdoc />
    public partial class Audit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Actiune",
                table: "Audite");

            migrationBuilder.DropColumn(
                name: "Utilizator",
                table: "Audite");

            migrationBuilder.RenameColumn(
                name: "Data",
                table: "Audite",
                newName: "DataAudit");

            migrationBuilder.AddColumn<string>(
                name: "Descriere",
                table: "Audite",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descriere",
                table: "Audite");

            migrationBuilder.RenameColumn(
                name: "DataAudit",
                table: "Audite",
                newName: "Data");

            migrationBuilder.AddColumn<string>(
                name: "Actiune",
                table: "Audite",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Utilizator",
                table: "Audite",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
