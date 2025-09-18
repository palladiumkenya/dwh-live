
using System.ComponentModel.DataAnnotations;
using LiveDWAPI.Domain.Common;

namespace LiveDWAPI.Domain.Stats;

public class ReportingHistory
{
    [Key]
    public DateTime Period { get; set; }
    public int FacilityCount { get; set; }
    public int CountyCount { get; set; }
    public int PartnerCount { get; set; }
    public int? CurrentFacilityCount { get; set; }
    public int? CurrentCountyCount { get; set; }
    public int? CurrentPartnerCount { get; set; }
    public DateTime LastUpdate { get; set; }

    public static ReportingHistory Generate(List<SiteReporting> liveReport)
    {
        var history = new ReportingHistory()
        {
            Period = Utils.GetLastReportingMonth(),
            FacilityCount = liveReport.Count,
            CountyCount = liveReport.Select(x=>x.County).Distinct().Count(),
            PartnerCount = liveReport.Select(x=>x.Partner).Distinct().Count(),
            LastUpdate = DateTime.Now
        };
        return history;
    }

    public void UpdateStats(ReportingHistory newReport)
    {
        Period = newReport.Period;
        FacilityCount = newReport.FacilityCount;
        CountyCount = newReport.CountyCount;
        PartnerCount = newReport.PartnerCount;
        CurrentFacilityCount = newReport.CurrentFacilityCount;
        CurrentCountyCount = newReport.CurrentCountyCount;
        CurrentPartnerCount = newReport.CurrentPartnerCount;
        LastUpdate = DateTime.Now;
    }
}