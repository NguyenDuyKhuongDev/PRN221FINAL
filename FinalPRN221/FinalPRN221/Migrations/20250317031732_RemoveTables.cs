using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalPRN221.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("Log");
            migrationBuilder.DropTable("LogAction");
            migrationBuilder.DropTable("LogLevel");
        }
    }

    /// <inheritdoc />
}