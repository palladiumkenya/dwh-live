using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LiveDWAPI.Infrastructure.Migrations.Gf
{
    /// <inheritdoc />
    public partial class GfInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                    create or alter VIEW vAgeGroupGf 
                    as 
                    SELECT distinct AgeGroup FROM CSAggregateBMGFIndicators");

            migrationBuilder.Sql(@"
                    create or alter VIEW vAgencyPartnerGf
                    as
                    SELECT distinct AgencyName,PartnerName FROM CSAggregateBMGFIndicators");

            migrationBuilder.Sql(@"
                    create or alter VIEW vIndicatorGf 
                    as
                    SELECT distinct Indicator FROM CSAggregateBMGFIndicators");

            migrationBuilder.Sql(@"
                    create or alter VIEW vRegionFacilityGf 
                    as
                    SELECT distinct County,SubCounty,Ward,FacilityName FROM CSAggregateBMGFIndicators");

            migrationBuilder.Sql(@"
                    create or alter VIEW vSexGf 
                    as
                    SELECT distinct Sex FROM CSAggregateBMGFIndicators");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"drop VIEW vAgeGroupGf");
            migrationBuilder.Sql(@"drop VIEW vAgencyPartnerGf");
            migrationBuilder.Sql(@"drop VIEW vIndicatorGf");
            migrationBuilder.Sql(@"drop VIEW vRegionFacilityGf");
            migrationBuilder.Sql(@"drop VIEW vSexGf");
        }
    }
}
