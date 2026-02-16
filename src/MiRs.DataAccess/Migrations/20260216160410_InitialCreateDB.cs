using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiRs.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Domain = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GuildCompletedEventArchive",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GuildId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    Eventname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventComplete = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    EventStart = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    EventEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    EventTeamWinner = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuildCompletedEventArchive", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GuildEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GuildId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    Eventname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParticipantPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventActive = table.Column<bool>(type: "bit", nullable: false),
                    EventPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    EventStart = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    EventEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuildEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GuildPermissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GuildId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    ChannelId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MessageId = table.Column<decimal>(type: "decimal(20,0)", nullable: true),
                    TeamId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuildPermissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GuildTeams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GuildId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    TeamName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuildTeams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RunescapeLootAlias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Lootname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lootalias = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mobname = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RunescapeLootAlias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRawLoot",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    Loot = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Mobname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MobLevel = table.Column<int>(type: "int", nullable: false),
                    DateLogged = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Processed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRawLoot", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PreviousUsername = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Runescapename = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PreviousRunescapename = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Level",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Levelnumber = table.Column<int>(type: "int", nullable: false),
                    Unlock = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnlockDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Level", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Level_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                        name: "FK_GuildEventTeam_GuildEvents_EventId",
                        column: x => x.EventId,
                        principalTable: "GuildEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GuildEventTeam_GuildTeams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "GuildTeams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserToTeams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    TeamId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToTeams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserToTeams_GuildTeams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "GuildTeams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserToTeams_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LevelTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Goal = table.Column<int>(type: "int", nullable: false),
                    LevelId = table.Column<int>(type: "int", nullable: false),
                    Levelnumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LevelTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LevelTasks_Level_LevelId",
                        column: x => x.LevelId,
                        principalTable: "Level",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GuildTeamCategoryProgress",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsComplete = table.Column<bool>(type: "bit", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    GuildEventTeamId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuildTeamCategoryProgress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GuildTeamCategoryProgress_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GuildTeamCategoryProgress_GuildEventTeam_GuildEventTeamId",
                        column: x => x.GuildEventTeamId,
                        principalTable: "GuildEventTeam",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GuildTeamCategoryLevelProgress",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsComplete = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    LastUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LevelId = table.Column<int>(type: "int", nullable: false),
                    CategoryProgressId = table.Column<int>(type: "int", nullable: false),
                    GuildEventTeamId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuildTeamCategoryLevelProgress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GuildTeamCategoryLevelProgress_GuildTeamCategoryProgress_CategoryProgressId",
                        column: x => x.CategoryProgressId,
                        principalTable: "GuildTeamCategoryProgress",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GuildTeamCategoryLevelProgress_Level_LevelId",
                        column: x => x.LevelId,
                        principalTable: "Level",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GuildTeamLevelTaskProgress",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Progress = table.Column<int>(type: "int", nullable: false),
                    IsComplete = table.Column<bool>(type: "bit", nullable: false),
                    LastUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    GuildEventTeamId = table.Column<int>(type: "int", nullable: false),
                    CategoryLevelProcessId = table.Column<int>(type: "int", nullable: false),
                    LevelTaskId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuildTeamLevelTaskProgress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GuildTeamLevelTaskProgress_GuildTeamCategoryLevelProgress_CategoryLevelProcessId",
                        column: x => x.CategoryLevelProcessId,
                        principalTable: "GuildTeamCategoryLevelProgress",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GuildTeamLevelTaskProgress_LevelTasks_LevelTaskId",
                        column: x => x.LevelTaskId,
                        principalTable: "LevelTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GuildEventTeam_EventId",
                table: "GuildEventTeam",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_GuildEventTeam_TeamId",
                table: "GuildEventTeam",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_GuildTeamCategoryLevelProgress_CategoryProgressId",
                table: "GuildTeamCategoryLevelProgress",
                column: "CategoryProgressId");

            migrationBuilder.CreateIndex(
                name: "IX_GuildTeamCategoryLevelProgress_LevelId",
                table: "GuildTeamCategoryLevelProgress",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_GuildTeamCategoryProgress_CategoryId",
                table: "GuildTeamCategoryProgress",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_GuildTeamCategoryProgress_GuildEventTeamId",
                table: "GuildTeamCategoryProgress",
                column: "GuildEventTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_GuildTeamLevelTaskProgress_CategoryLevelProcessId",
                table: "GuildTeamLevelTaskProgress",
                column: "CategoryLevelProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_GuildTeamLevelTaskProgress_LevelTaskId",
                table: "GuildTeamLevelTaskProgress",
                column: "LevelTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Level_CategoryId",
                table: "Level",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_LevelTasks_LevelId",
                table: "LevelTasks",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToTeams_TeamId",
                table: "UserToTeams",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToTeams_UserId",
                table: "UserToTeams",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GuildCompletedEventArchive");

            migrationBuilder.DropTable(
                name: "GuildPermissions");

            migrationBuilder.DropTable(
                name: "GuildTeamLevelTaskProgress");

            migrationBuilder.DropTable(
                name: "RunescapeLootAlias");

            migrationBuilder.DropTable(
                name: "UserRawLoot");

            migrationBuilder.DropTable(
                name: "UserToTeams");

            migrationBuilder.DropTable(
                name: "GuildTeamCategoryLevelProgress");

            migrationBuilder.DropTable(
                name: "LevelTasks");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "GuildTeamCategoryProgress");

            migrationBuilder.DropTable(
                name: "Level");

            migrationBuilder.DropTable(
                name: "GuildEventTeam");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "GuildEvents");

            migrationBuilder.DropTable(
                name: "GuildTeams");
        }
    }
}
