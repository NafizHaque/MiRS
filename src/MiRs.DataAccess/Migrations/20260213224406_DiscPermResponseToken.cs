using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiRs.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class DiscPermResponseToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MessageId",
                table: "GuildPermissions");

            migrationBuilder.AddColumn<string>(
                name: "ResponseToken",
                table: "GuildPermissions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResponseToken",
                table: "GuildPermissions");

            migrationBuilder.AddColumn<decimal>(
                name: "MessageId",
                table: "GuildPermissions",
                type: "decimal(20,0)",
                nullable: true);
        }
    }
}
