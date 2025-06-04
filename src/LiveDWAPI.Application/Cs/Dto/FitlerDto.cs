namespace LiveDWAPI.Application.Cs.Dto;

public class FilterDto
{
    public string? Indicator { get;set;  }
    // Time
    public DateTime? StartPeriod { get; set; }
    public DateTime? EndPeriod { get; set; }
    // Place
    public string[]? County { get; set; }
    public string[]? SubCounty { get; set; }
    public string[]? Ward { get; set; }
    public string[]? FacilityName { get; set; }
    // Person
    public string[]? Sex { get; set; }
    public string[]? AgeGroup { get; set; }
    // Partner
    public string[]? Agency { get;  set;}
    public string[]? PartnerName { get; set; }

    public bool HasIndicator() => !string.IsNullOrWhiteSpace(Indicator);
    public bool HasStartPeriod() => StartPeriod.HasValue;
    public bool HasEndPeriod() => EndPeriod.HasValue;
    public bool HasCounty() => County?.Length > 0;
    public bool HasSubCounty() => SubCounty?.Length > 0;
    public bool HasWard() => Ward?.Length > 0;
    public bool HasFacilityName() => FacilityName?.Length > 0;
    public bool HasSex() => Sex?.Length > 0;
    public bool HasAgeGroup() => AgeGroup?.Length > 0;
    public bool HasAgency() => Agency?.Length > 0;
    public bool HasPartnerName() => PartnerName?.Length > 0;
}

