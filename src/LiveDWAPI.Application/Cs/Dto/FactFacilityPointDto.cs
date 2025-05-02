namespace LiveDWAPI.Application.Cs.Dto;

public class FactFacilityPointDto
{
    public string? FacilityName { get; set; }
        
    public string? PartnerName { get; set; }
    public string? Agency { get; set; }
        
    public string? County { get; set; }
    public string? SubCounty { get; set; }
        
    public decimal? Lat { get; set; }
    public decimal? Long { get; set; }

}