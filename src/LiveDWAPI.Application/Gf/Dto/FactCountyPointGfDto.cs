using LiveDWAPI.Domain.Gf;

namespace LiveDWAPI.Application.Gf.Dto;

public class FactCountyPointGfDto
{
    public string? County { get; set; }
    public int? Count { get; set; }
    public double? Rate { get; set; }

    public static List<FactCountyPointGfDto> Generate(List<FactAggregateIndicator> indicators)
    {
        var points = indicators
            .GroupBy(x=>new{x.County})
            .Select(g=> new FactCountyPointGfDto()
            {
                County = g.Key.County,
                Count = g.Sum(x=>x.Numerator),
                Rate = 
                    (g.Sum(x=>x.Numerator)*1.0)/
                    (g.Sum(x=>x.Denominator)*1.0) *100
                       
            })
            .ToList();

        return points;
    }
}