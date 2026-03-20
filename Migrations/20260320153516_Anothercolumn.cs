using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Noman.Migrations
{
    /// <inheritdoc />
    public partial class Anothercolumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StudentMaritialStatus",
                table: "Students",
                type: "varchar(20)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudentMaritialStatus",
                table: "Students");
        }
    }
}
