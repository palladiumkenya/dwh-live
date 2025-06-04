using LiveDWAPI.Domain.Cs;

namespace LiveDWAPI.Application.Cs.Dto;

public class FactSubCountyPointDto
{
    public string? SubCounty { get; set; }
    public string? County { get; set; }
    public int? Count { get; set; }
    public double? Rate { get; set; }

    public static List<FactSubCountyPointDto> Generate(List<FactRealtimeIndicator> indicators)
    {
        var points = indicators
            .GroupBy(x=>new{x.SubCounty})
            .Select(g=> new FactSubCountyPointDto()
            {
                SubCounty = g.Key.SubCounty,
                County=g.First().County,
                Count = g.Sum(x=>x.Numerator),
                Rate = 
                    (g.Sum(x=>x.Numerator)*1.0)/
                    (g.Sum(x=>x.Denominator)*1.0) *100
                       
            })
            .ToList();
        return points;
    }
}

public class FactWardPointDto
{
    public string? Ward { get; set; }
    public string? SubCounty { get; set; }
    public string? County { get; set; }
    public int? Count { get; set; }
    public double? Rate { get; set; }

    public static List<FactWardPointDto> Generate(List<FactRealtimeIndicator> indicators)
    {
        var points = indicators
            .GroupBy(x=>new{x.Ward})
            .Select(g=> new FactWardPointDto()
            {
                Ward = g.Key.Ward,
                SubCounty = g.First().SubCounty,
                County=g.First().County,
                Count = g.Sum(x=>x.Numerator),
                Rate = 
                    (g.Sum(x=>x.Numerator)*1.0)/
                    (g.Sum(x=>x.Denominator)*1.0) *100
                       
            })
            .ToList();
        return points;
    }
}