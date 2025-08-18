using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiRs.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PreviousRunescapename",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Runescapename",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreviousRunescapename",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Runescapename",
                table: "Users");
        }
    }
}
