using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCore.Migrations
{
    /// <inheritdoc />
    public partial class Updateingreportagain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Reports_ReporterUserId_TargetType_TargetId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Reports_TargetType_TargetId_Status",
                table: "Reports");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_ReporterUserId",
                table: "Reports",
                column: "ReporterUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Reports_ReporterUserId",
                table: "Reports");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_ReporterUserId_TargetType_TargetId",
                table: "Reports",
                columns: new[] { "ReporterUserId", "TargetType", "TargetId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reports_TargetType_TargetId_Status",
                table: "Reports",
                columns: new[] { "TargetType", "TargetId", "Status" });
        }
    }
}
