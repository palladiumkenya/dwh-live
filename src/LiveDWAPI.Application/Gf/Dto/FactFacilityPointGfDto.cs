using LiveDWAPI.Domain.Gf;

namespace LiveDWAPI.Application.Gf.Dto;

public class FactFacilityPointGfDto
{
    public string? FacilityName { get; set; }
        
    public string? PartnerName { get; set; }
    public string? Agency { get; set; }
        
    public string? County { get; set; }
    public string? SubCounty { get; set; }
    public decimal? Lat { get; set; }
    public decimal? Long { get; set; }
    public int? Count { get; set; }
    public double? Rate { get; set; }

    public static List<FactFacilityPointGfDto> Generate(List<FactAggregateIndicator> indicators)
    {
        var points = indicators
            .GroupBy(x=>new{x.FacilityName})
            .Select(g=> new FactFacilityPointGfDto()
            {
                FacilityName = g.Key.FacilityName,
                PartnerName=g.First().PartnerName,
                Agency=g.First().Agency,
                
                County=g.First().County,
                SubCounty= g.First().SubCounty,
                Lat= g.First().Lat,
                Long= g.First().Long,
                Count = g.Sum(x=>x.Numerator),
                Rate = 
                    (g.Sum(x=>x.Numerator)*1.0)/
                    (g.Sum(x=>x.Denominator)*1.0) *100
                       
            })
            .ToList();
        
        return points;
    }
}