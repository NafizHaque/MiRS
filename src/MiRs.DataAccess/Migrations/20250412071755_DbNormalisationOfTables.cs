using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiRs.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class DbNormalisationOfTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GuildTeamsLevelProgress_Levels_LevelId",
                table: "GuildTeamsLevelProgress");

            migrationBuilder.DropTable(
                name: "Levels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LevelTask",
                table: "LevelTask");

            migrationBuilder.RenameTable(
                name: "LevelTask",
                newName: "LevelTasks");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Categories",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "LevelTasks",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "LevelTasks",
                newName: "Id");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "LevelTasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Goal",
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

            migrationBuilder.AddColumn<int>(
                name: "Unlock",
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

            migrationBuilder.AddPrimaryKey(
                name: "PK_LevelTasks",
                table: "LevelTasks",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_LevelTasks_CategoryId",
                table: "LevelTasks",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_GuildTeamsLevelProgress_LevelTasks_LevelId",
                table: "GuildTeamsLevelProgress",
                column: "LevelId",
                principalTable: "LevelTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LevelTasks_Categories_CategoryId",
                table: "LevelTasks",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GuildTeamsLevelProgress_LevelTasks_LevelId",
                table: "GuildTeamsLevelProgress");

            migrationBuilder.DropForeignKey(
                name: "FK_LevelTasks_Categories_CategoryId",
                table: "LevelTasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LevelTasks",
                table: "LevelTasks");

            migrationBuilder.DropIndex(
                name: "IX_LevelTasks_CategoryId",
                table: "LevelTasks");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "LevelTasks");

            migrationBuilder.DropColumn(
                name: "Goal",
                table: "LevelTasks");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "LevelTasks");

            migrationBuilder.DropColumn(
                name: "Unlock",
                table: "LevelTasks");

            migrationBuilder.DropColumn(
                name: "UnlockDescription",
                table: "LevelTasks");

            migrationBuilder.RenameTable(
                name: "LevelTasks",
                newName: "LevelTask");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Categories",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "LevelTask",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "LevelTask",
                newName: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LevelTask",
                table: "LevelTask",
                column: "id");

            migrationBuilder.CreateTable(
                name: "Levels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    Goal = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Levels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Levels_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Levels_LevelTask_TaskId",
                        column: x => x.TaskId,
                        principalTable: "LevelTask",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Levels_CategoryId",
                table: "Levels",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Levels_TaskId",
                table: "Levels",
                column: "TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_GuildTeamsLevelProgress_Levels_LevelId",
                table: "GuildTeamsLevelProgress",
                column: "LevelId",
                principalTable: "Levels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
