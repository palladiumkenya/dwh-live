using LiveDWAPI.Domain.Gf;

namespace LiveDWAPI.Application.Gf.Dto;

public class FactSubCountyPointGfDto
{
    public string? SubCounty { get; set; }
    public string? County { get; set; }
    public int? Count { get; set; }
    public double? Rate { get; set; }

    public static List<FactSubCountyPointGfDto> Generate(List<FactAggregateIndicator> indicators)
    {
        var points = indicators
            .GroupBy(x=>new{x.SubCounty})
            .Select(g=> new FactSubCountyPointGfDto()
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

public class FactWardPointGfDto
{
    public string? Ward { get; set; }
    public string? SubCounty { get; set; }
    public string? County { get; set; }
    public int? Count { get; set; }
    public double? Rate { get; set; }

    public static List<FactWardPointGfDto> Generate(List<FactAggregateIndicator> indicators)
    {
        var points = indicators
            .GroupBy(x=>new{x.Ward})
            .Select(g=> new FactWardPointGfDto()
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