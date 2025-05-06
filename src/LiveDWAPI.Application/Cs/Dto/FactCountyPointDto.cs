using LiveDWAPI.Domain.Cs;

namespace LiveDWAPI.Application.Cs.Dto;

public class FactCountyPointDto
{
    public string? County { get; set; }
    public int? Count { get; set; }
    public double? Rate { get; set; }

    public static List<FactCountyPointDto> Generate(List<FactRealtimeIndicator> indicators)
    {
        var points = indicators
            .GroupBy(x=>new{x.County})
            .Select(g=> new FactCountyPointDto()
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