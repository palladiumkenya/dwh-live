using CSharpFunctionalExtensions;
using LiveDWAPI.Domain.Cs;

namespace LiveDWAPI.Application.Cs.Dto
{
    public class IndicatorDataDto
    {
        public List<FactCountyPointDto> CountyPoints { get; set; } = new ();
        public List<FactSubCountyPointDto> SubCountyPoints { get; set; }= new ();
        public List<FactFacilityPointDto> FacilityPoints { get; set; }= new ();
    }
}
