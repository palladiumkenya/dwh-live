using AutoMapper;
using LiveDWAPI.Domain.Gf;

namespace LiveDWAPI.Application.Gf.Dto;

public class CsProfile:Profile
{
    public CsProfile()
    {
        CreateMap<FactAggregateIndicator, FactFacilityPointGfDto>();
        CreateMap<DimIndicator, DimIndicatorGf>();
        CreateMap<DimRegion, DimRegionGf>();
        CreateMap<DimAgency, DimAgencyGf>();
        CreateMap<DimAgeGroup, DimAgeGroupGf>();
        CreateMap<DimSex, DimSexGf>();
    }
}