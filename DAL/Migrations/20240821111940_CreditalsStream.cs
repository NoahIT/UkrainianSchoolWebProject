using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class CreditalsStream : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "StreamInformations",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "StreamInformations",
                type: "longtext",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "StreamInformations");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "StreamInformations");
        }
    }
}
