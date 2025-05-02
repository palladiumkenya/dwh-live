using AutoMapper;
using LiveDWAPI.Domain.Cs;

namespace LiveDWAPI.Application.Cs.Dto;

public class CsProfile:Profile
{
    public CsProfile()
    {
        CreateMap<FactRealtimeIndicator, FactFacilityPointDto>();
    }
}