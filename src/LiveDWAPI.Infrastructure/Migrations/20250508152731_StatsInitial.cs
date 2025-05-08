using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LiveDWAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class StatsInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReportingHistories",
                columns: table => new
                {
                    Period = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FacilityCount = table.Column<int>(type: "int", nullable: false),
                    CountyCount = table.Column<int>(type: "int", nullable: false),
                    PartnerCount = table.Column<int>(type: "int", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportingHistories", x => x.Period);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReportingHistories");
        }
    }
}
