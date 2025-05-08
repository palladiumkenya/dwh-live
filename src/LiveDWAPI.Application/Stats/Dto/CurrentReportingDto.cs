namespace LiveDWAPI.Application.Stats.Dto;

public class CurrentReportingDto
{
    public DateTime Period { get; set; }
    public int FacilityCount { get; set; }
    public int CountyCount { get; set; }
    public int PartnerCount { get; set; }
}