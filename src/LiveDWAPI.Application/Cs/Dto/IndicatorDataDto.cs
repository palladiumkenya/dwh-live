using LiveDWAPI.Domain.Cs;

namespace LiveDWAPI.Application.Cs.Dto
{
    public class IndicatorDataDto
    {
        public List<FactCountyPointDto> CountyPoints { get; set; } = new ();
        public List<FactSubCountyPointDto> SubCountyPoints { get; set; }= new ();
        public List<FactWardPointDto> WardPoints { get; set; }= new ();
        public List<FactFacilityPointDto> FacilityPoints { get; set; }= new ();

        public IndicatorDataDto()
        {
        }


        public IndicatorDataDto(List<FactRealtimeIndicator> indicators)
        {
            CountyPoints = FactCountyPointDto.Generate(indicators);
            SubCountyPoints = FactSubCountyPointDto.Generate(indicators);
            WardPoints = FactWardPointDto.Generate(indicators);
            FacilityPoints = FactFacilityPointDto.Generate(indicators);
        }
    }
}
