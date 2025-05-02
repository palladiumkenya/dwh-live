using AutoMapper;
using CSharpFunctionalExtensions;
using FluentValidation;
using LiveDWAPI.Application.Cs.Dto;
using LiveDWAPI.Domain.Cs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace LiveDWAPI.Application.Cs.Queries;

public class GetCountyPointQuery:IRequest<Result<List<FactCountyPointDto>>>
{
    public string Indicator { get;  }

    public GetCountyPointQuery(string indicator)
    {
        Indicator = indicator;
    }
}

public class GetCountyPointQueryHandler:IRequestHandler<GetCountyPointQuery,Result<List<FactCountyPointDto>>>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public GetCountyPointQueryHandler(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<Result<List<FactCountyPointDto>>> Handle(GetCountyPointQuery request, CancellationToken cancellationToken)
    {
        try
        {
           var res = await _mediator.Send(new GetRealtimeIndicatorQuery(request.Indicator), cancellationToken);
           if (res.IsSuccess)
           {
               var points = res.Value
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

               return Result.Success(points);
           }

           throw new Exception(res.Error);
        }
        catch (Exception e)
        {
            Log.Error(e,"Load Realtime Indicators error!");
            return Result.Failure<List<FactCountyPointDto>>(e.Message);
        }
    }
}

public class GetCountyPointQueryValidator: AbstractValidator<GetCountyPointQuery>
{
    public GetCountyPointQueryValidator()
    {
        RuleFor(x => x.Indicator).NotEmpty()
            .WithMessage("Indicator is required");
    }
}