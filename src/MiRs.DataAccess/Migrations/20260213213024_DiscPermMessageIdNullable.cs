using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiRs.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class DiscPermMessageIdNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "MessageId",
                table: "GuildPermissions",
                type: "decimal(20,0)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,0)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "MessageId",
                table: "GuildPermissions",
                type: "decimal(20,0)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,0)",
                oldNullable: true);
        }
    }
}
