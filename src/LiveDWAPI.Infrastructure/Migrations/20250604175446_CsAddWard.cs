using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LiveDWAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CsAddWard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                    create or alter VIEW vRegionFacility 
                    as
                    SELECT distinct County,SubCounty,Ward,FacilityName FROM CSRTCombinedIndicators");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
