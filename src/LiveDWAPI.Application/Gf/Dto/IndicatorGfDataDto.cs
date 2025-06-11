using LiveDWAPI.Domain.Gf;

namespace LiveDWAPI.Application.Gf.Dto
{
    public class IndicatorGfDataDto
    {
        public List<FactCountyPointGfDto> CountyPoints { get; set; } = new ();
        public List<FactSubCountyPointGfDto> SubCountyPoints { get; set; }= new ();
        public List<FactWardPointGfDto> WardPoints { get; set; }= new ();
        public List<FactFacilityPointGfDto> FacilityPoints { get; set; }= new ();

        public IndicatorGfDataDto()
        {
        }


        public IndicatorGfDataDto(List<FactAggregateIndicator> indicators)
        {
            CountyPoints = FactCountyPointGfDto.Generate(indicators);
            SubCountyPoints = FactSubCountyPointGfDto.Generate(indicators);
            WardPoints = FactWardPointGfDto.Generate(indicators);
            FacilityPoints = FactFacilityPointGfDto.Generate(indicators);
        }
    }
}
