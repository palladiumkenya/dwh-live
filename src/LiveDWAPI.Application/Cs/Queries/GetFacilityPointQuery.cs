using AutoMapper;
using CSharpFunctionalExtensions;
using FluentValidation;
using LiveDWAPI.Application.Cs.Dto;
using LiveDWAPI.Domain.Cs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace LiveDWAPI.Application.Cs.Queries;

public class GetFacilityPointQuery:IRequest<Result<List<FactFacilityPointDto>>>
{
    public string Indicator { get;  }

    public GetFacilityPointQuery(string indicator)
    {
        Indicator = indicator;
    }
}

public class GetFacilityPointQueryHandler:IRequestHandler<GetFacilityPointQuery,Result<List<FactFacilityPointDto>>>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public GetFacilityPointQueryHandler(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<Result<List<FactFacilityPointDto>>> Handle(GetFacilityPointQuery request, CancellationToken cancellationToken)
    {
        try
        {
           var res = await _mediator.Send(new GetRealtimeIndicatorQuery(request.Indicator), cancellationToken);
           if (res.IsSuccess)
           {
               var points = (_mapper.Map<List<FactFacilityPointDto>>(res.Value))
                   .Distinct()
                   .ToList();

               return Result.Success(points);
           }

           throw new Exception(res.Error);
        }
        catch (Exception e)
        {
            Log.Error(e,"Load Realtime Indicators error!");
            return Result.Failure<List<FactFacilityPointDto>>(e.Message);
        }
    }
}

public class GetFacilityPointQueryValidator: AbstractValidator<GetFacilityPointQuery>
{
    public GetFacilityPointQueryValidator()
    {
        RuleFor(x => x.Indicator).NotEmpty()
            .WithMessage("Indicator is required");
    }
}