using AutoMapper;
using CSharpFunctionalExtensions;
using FluentValidation;
using LiveDWAPI.Application.Cs.Dto;
using MediatR;
using Serilog;

namespace LiveDWAPI.Application.Cs.Queries;

public class GetSubCountyPointQuery:IRequest<Result<List<FactSubCountyPointDto>>>
{
    public string Indicator { get;  }

    public GetSubCountyPointQuery(string indicator)
    {
        Indicator = indicator;
    }
}

public class GetSubCountyPointQueryHandler:IRequestHandler<GetSubCountyPointQuery,Result<List<FactSubCountyPointDto>>>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public GetSubCountyPointQueryHandler(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<Result<List<FactSubCountyPointDto>>> Handle(GetSubCountyPointQuery request, CancellationToken cancellationToken)
    {
        try
        {
           var res = await _mediator.Send(new GetRealtimeIndicatorQuery(request.Indicator), cancellationToken);
           if (res.IsSuccess)
           {
               var points = res.Value
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

               return Result.Success(points);
           }

           throw new Exception(res.Error);
        }
        catch (Exception e)
        {
            Log.Error(e,"Load Realtime Indicators error!");
            return Result.Failure<List<FactSubCountyPointDto>>(e.Message);
        }
    }
}

public class GetSubCountyPointQueryValidator: AbstractValidator<GetSubCountyPointQuery>
{
    public GetSubCountyPointQueryValidator()
    {
        RuleFor(x => x.Indicator).NotEmpty()
            .WithMessage("Indicator is required");
    }
}