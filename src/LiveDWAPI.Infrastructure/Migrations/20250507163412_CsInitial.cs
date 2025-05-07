using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LiveDWAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CsInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                    create or alter VIEW vAgeGroup 
                    as 
                    SELECT distinct AgeGroup FROM CSRTCombinedIndicators");

            migrationBuilder.Sql(@"
                    create or alter VIEW vAgencyPartner 
                    as
                    SELECT distinct Agency,PartnerName FROM CSRTCombinedIndicators");

            migrationBuilder.Sql(@"
                    create or alter VIEW vIndicator 
                    as
                    SELECT distinct Indicator FROM CSRTCombinedIndicators");

            migrationBuilder.Sql(@"
                    create or alter VIEW vRegionFacility 
                    as
                    SELECT distinct County,SubCounty,FacilityName FROM CSRTCombinedIndicators");

            migrationBuilder.Sql(@"
                    create or alter VIEW vSex 
                    as
                    SELECT distinct Sex FROM CSRTCombinedIndicators");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"drop VIEW vAgeGroup");
            migrationBuilder.Sql(@"drop VIEW vAgencyPartner");
            migrationBuilder.Sql(@"drop VIEW vIndicator");
            migrationBuilder.Sql(@"drop VIEW vRegionFacility");
            migrationBuilder.Sql(@"drop VIEW vSex");
        }
    }
}
