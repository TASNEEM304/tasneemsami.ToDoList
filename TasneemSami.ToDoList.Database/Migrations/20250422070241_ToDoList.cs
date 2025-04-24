
using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TasneemSami.ToDoList.Database.Migrations
{
    public partial class ToDoList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PASSWORD = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ROLE = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TITLE = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    DESCRIPTION = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ISCOMPLETED = table.Column<bool>(type: "bit", nullable: false),
                    DUEDATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CATEGORY = table.Column<int>(type: "int", nullable: false),
                    PRIORITY = table.Column<int>(type: "int", nullable: false),
                    USERID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Tasks_Users_USERID",
                        column: x => x.USERID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_USERID",
                table: "Tasks",
                column: "USERID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
