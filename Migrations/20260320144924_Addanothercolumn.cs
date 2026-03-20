using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Noman.Migrations
{
    /// <inheritdoc />
    public partial class Addanothercolumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<string>(
                name: "StudentGender",
                table: "Students",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudentGender",
                table: "Students");

            migrationBuilder.AlterColumn<string>(
                name: "StudentImage",
                table: "Students",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true);
        }
    }
}
