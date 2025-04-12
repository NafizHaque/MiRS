using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiRs.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class CreatedEventDataTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GuildEvent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GuildId = table.Column<int>(type: "int", nullable: false),
                    Eventname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventActive = table.Column<bool>(type: "bit", nullable: false),
                    EventStart = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    EventEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    TeamId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuildEvent", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GuildEventTeam",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    TeamId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuildEventTeam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GuildEventTeam_GuildEvent_EventId",
                        column: x => x.EventId,
                        principalTable: "GuildEvent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GuildEventTeam_GuildTeams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "GuildTeams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GuildEventTeam_EventId",
                table: "GuildEventTeam",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_GuildEventTeam_TeamId",
                table: "GuildEventTeam",
                column: "TeamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GuildEventTeam");

            migrationBuilder.DropTable(
                name: "GuildEvent");
        }
    }
}
