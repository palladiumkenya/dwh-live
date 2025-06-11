using AutoMapper;
using CSharpFunctionalExtensions;
using FluentValidation;
using LiveDWAPI.Application.Cs.Dto;
using MediatR;
using Serilog;

namespace LiveDWAPI.Application.Cs.Queries;

public class GetWardPointQuery:IRequest<Result<List<FactWardPointDto>>>
{
    public string Indicator { get;  }

    public GetWardPointQuery(string indicator)
    {
        Indicator = indicator;
    }
}

public class GetWardPointQueryHandler:IRequestHandler<GetWardPointQuery,Result<List<FactWardPointDto>>>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public GetWardPointQueryHandler(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<Result<List<FactWardPointDto>>> Handle(GetWardPointQuery request, CancellationToken cancellationToken)
    {
        try
        {
           var res = await _mediator.Send(new GetRealtimeIndicatorQuery(request.Indicator), cancellationToken);
           if (res.IsSuccess)
           {
               var points = res.Value
                   .GroupBy(x=>new{x.Ward})
                   .Select(g=> new FactWardPointDto()
                   {
                       Ward = g.Key.Ward,
                       SubCounty= g.First().SubCounty,
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
            return Result.Failure<List<FactWardPointDto>>(e.Message);
        }
    }
}

public class GetWardPointQueryValidator: AbstractValidator<GetWardPointQuery>
{
    public GetWardPointQueryValidator()
    {
        RuleFor(x => x.Indicator).NotEmpty()
            .WithMessage("Indicator is required");
    }
}