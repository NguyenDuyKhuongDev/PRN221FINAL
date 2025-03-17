using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalPRN221.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDB_afterDeleteTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Log");

            migrationBuilder.DropTable(
                name: "LogAction");

            migrationBuilder.DropTable(
                name: "LogLevel");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
      name: "LogAction",
      columns: table => new
      {
          ID = table.Column<int>(type: "int", nullable: false)
              .Annotation("SqlServer:Identity", "1, 1"),
          Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
      },
      constraints: table =>
      {
          table.PrimaryKey("PK_LogAction", x => x.ID);
      });

            migrationBuilder.CreateTable(
                name: "LogLevel",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogLevel", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Log",
                columns: table => new
                {
                    LogID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActionID = table.Column<int>(type: "int", nullable: false),
                    LogLevelID = table.Column<int>(type: "int", nullable: false),
                    IPAdress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeStamp = table.Column<DateOnly>(type: "date", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log", x => x.LogID);
                    table.ForeignKey(
                        name: "FK_Log_LogAction",
                        column: x => x.ActionID,
                        principalTable: "LogAction",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Log_LogLevel",
                        column: x => x.LogLevelID,
                        principalTable: "LogLevel",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Log_ActionID",
                table: "Log",
                column: "ActionID");

            migrationBuilder.CreateIndex(
                name: "IX_Log_LogLevelID",
                table: "Log",
                column: "LogLevelID");
        }
    }
}