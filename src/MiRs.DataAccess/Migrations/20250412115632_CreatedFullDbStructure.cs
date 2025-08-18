using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiRs.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class CreatedFullDbStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LevelTasks_Categories_CategoryId",
                table: "LevelTasks");

            migrationBuilder.DropTable(
                name: "GuildTeamsLevelProgress");

            migrationBuilder.DropIndex(
                name: "IX_LevelTasks_CategoryId",
                table: "LevelTasks");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "LevelTasks");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "LevelTasks");

            migrationBuilder.DropColumn(
                name: "UnlockDescription",
                table: "LevelTasks");

            migrationBuilder.RenameColumn(
                name: "Unlock",
                table: "LevelTasks",
                newName: "LevelId");

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
                name: "Level",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Levelnumber = table.Column<int>(type: "int", nullable: false),
                    Unlock = table.Column<int>(type: "int", nullable: false),
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
                name: "GuildTeamCategoryLevelProgress",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsComplete = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    LastUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LevelId = table.Column<int>(type: "int", nullable: false),
                    CategoryProgressId = table.Column<int>(type: "int", nullable: false)
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
                name: "IX_LevelTasks_LevelId",
                table: "LevelTasks",
                column: "LevelId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_LevelTasks_Level_LevelId",
                table: "LevelTasks",
                column: "LevelId",
                principalTable: "Level",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LevelTasks_Level_LevelId",
                table: "LevelTasks");

            migrationBuilder.DropTable(
                name: "GuildTeamLevelTaskProgress");

            migrationBuilder.DropTable(
                name: "GuildTeamCategoryLevelProgress");

            migrationBuilder.DropTable(
                name: "GuildTeamCategoryProgress");

            migrationBuilder.DropTable(
                name: "Level");

            migrationBuilder.DropIndex(
                name: "IX_LevelTasks_LevelId",
                table: "LevelTasks");

            migrationBuilder.RenameColumn(
                name: "LevelId",
                table: "LevelTasks",
                newName: "Unlock");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "LevelTasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "LevelTasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UnlockDescription",
                table: "LevelTasks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "GuildTeamsLevelProgress",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GuildEventTeamId = table.Column<int>(type: "int", nullable: false),
                    LevelId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsComplete = table.Column<bool>(type: "bit", nullable: false),
                    LastUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Progress = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuildTeamsLevelProgress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GuildTeamsLevelProgress_GuildEventTeam_GuildEventTeamId",
                        column: x => x.GuildEventTeamId,
                        principalTable: "GuildEventTeam",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GuildTeamsLevelProgress_LevelTasks_LevelId",
                        column: x => x.LevelId,
                        principalTable: "LevelTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LevelTasks_CategoryId",
                table: "LevelTasks",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_GuildTeamsLevelProgress_GuildEventTeamId",
                table: "GuildTeamsLevelProgress",
                column: "GuildEventTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_GuildTeamsLevelProgress_LevelId",
                table: "GuildTeamsLevelProgress",
                column: "LevelId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LevelTasks_Categories_CategoryId",
                table: "LevelTasks",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
