using LiveDWAPI.Application.Cs.Dto;
using LiveDWAPI.Application.Cs.Queries;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace LiveDWAPI.Application.Tests.Cs.Queries;

[TestFixture]
public class GetRealtimeFilteredQueryTests
{
    private IMediator? _mediator;
    
    [SetUp]
    public void SetUp()
    {
        _mediator = TestInitializer.ServiceProvider.GetRequiredService<IMediator>();
    }

    [TestCase("HIV POSITIVE NOT LINKED","Nairobi")]
    public async Task should_Read(string name,string county)
    {
        var filter = new FilterDto()
        {
            Indicator = name
        };
        var res =await _mediator!.Send(new GetRealtimeFilteredQuery(filter));
        Assert.That(res.IsSuccess,Is.True);
        Assert.That(res.Value.Any(),Is.True);
    }
   
    [TestCase("HIV POSITIVE NOT LINKED","Nairobi","Embakasi Central")]
    public async Task should_Read(string name,string county,string? subcounty)
    {
      var filter = new FilterDto()
      {
        Indicator = name
      };
      var res =await _mediator!.Send(new GetRealtimeFilteredQuery(filter));
      Assert.That(res.IsSuccess,Is.True);
      Assert.That(res.Value.Any(),Is.True);
    }
    
    [TestCase("HIV POSITIVE NOT LINKED","2025-04-07","2025-04-07")]
    public async Task should_Read_Period(string name,string start,string end)
    {
      var filter = new FilterDto()
      {
        Indicator = name,
        StartPeriod = DateTime.Parse(start),
        EndPeriod = DateTime.Parse(end)
      };
      var res =await _mediator!.Send(new GetRealtimeFilteredQuery(filter));
      Assert.That(res.IsSuccess,Is.True);
      Assert.That(res.Value.Any(),Is.True);
    }
    [TestCase("HIV POSITIVE NOT LINKED",1)]
    public async Task should_Read_With_Limit(string name,int  limit)
    {
      var filter = new FilterDto()
      {
        Indicator = name,
        Limit = limit
      };
      var res =await _mediator!.Send(new GetRealtimeFilteredQuery(filter));
      Assert.That(res.IsSuccess,Is.True);
      Assert.That(res.Value.Count,Is.EqualTo(limit));
    }
    
/*
 [
  {
    "Indicator": "HIV POSITIVE NOT LINKED",
    "Numerator": 1,
    "Denominator": 1,
    "AssessmentPeriod": "2025-04-07T00:00:00",
    "County": "Nairobi",
    "SubCounty": "Embakasi Central",
    "Sex": "Female",
    "AgeGroup": "35 to 39",
    "FacilityName": "Kayole II Sub-District Hospital",
    "PartnerName": "USAID Fahari ya Jamii",
    "Agency": "USAID",
    "Latitude": "-1.2665000000",
    "Longitude": "36.9167000000",
    "Lat": -1.2665000000,
    "Long": 36.9167000000,
    "Rate": 100.0
  },
  {
    "Indicator": "HIV POSITIVE NOT LINKED",
    "Numerator": 0,
    "Denominator": 1,
    "AssessmentPeriod": "2025-04-11T00:00:00",
    "County": "Kajiado",
    "SubCounty": "Kajiado North",
    "Sex": "Female",
    "AgeGroup": "25 to 29",
    "FacilityName": "Beacon of Hope Clinic (Ongata Rongai)",
    "PartnerName": "USAID Fahari ya Jamii",
    "Agency": "USAID",
    "Latitude": "-1.3940000000",
    "Longitude": "36.7628000000",
    "Lat": -1.3940000000,
    "Long": 36.7628000000,
    "Rate": 0.0
  },
  {
    "Indicator": "HIV POSITIVE NOT LINKED",
    "Numerator": 1,
    "Denominator": 1,
    "AssessmentPeriod": "2025-04-15T00:00:00",
    "County": "Mombasa",
    "SubCounty": "Kisauni",
    "Sex": "Male",
    "AgeGroup": "40 to 44",
    "FacilityName": "Shimo-La Tewa Health Centre (GK Prison)",
    "PartnerName": "LVCT Prisons",
    "Agency": "CDC",
    "Latitude": "-3.9562000000",
    "Longitude": "39.7382000000",
    "Lat": -3.9562000000,
    "Long": 39.7382000000,
    "Rate": 100.0
  },
  {
    "Indicator": "HIV POSITIVE NOT LINKED",
    "Numerator": 1,
    "Denominator": 1,
    "AssessmentPeriod": "2025-04-17T00:00:00",
    "County": "Mombasa",
    "SubCounty": "Kisauni",
    "Sex": "Male",
    "AgeGroup": "20 to 24",
    "FacilityName": "Shimo-La Tewa Health Centre (GK Prison)",
    "PartnerName": "LVCT Prisons",
    "Agency": "CDC",
    "Latitude": "-3.9562000000",
    "Longitude": "39.7382000000",
    "Lat": -3.9562000000,
    "Long": 39.7382000000,
    "Rate": 100.0
  },
  {
    "Indicator": "HIV POSITIVE NOT LINKED",
    "Numerator": 0,
    "Denominator": 1,
    "AssessmentPeriod": "2025-04-11T00:00:00",
    "County": "Nairobi",
    "SubCounty": "Starehe",
    "Sex": "Female",
    "AgeGroup": "45 to 49",
    "FacilityName": "The Mater Misericordiae Hospital",
    "PartnerName": "Coptic Orthodox Church",
    "Agency": "CDC",
    "Latitude": "-1.3067000000",
    "Longitude": "36.8343000000",
    "Lat": -1.3067000000,
    "Long": 36.8343000000,
    "Rate": 0.0
  },
  {
    "Indicator": "HIV POSITIVE NOT LINKED",
    "Numerator": 0,
    "Denominator": 1,
    "AssessmentPeriod": "2025-04-11T00:00:00",
    "County": "Nairobi",
    "SubCounty": "Starehe",
    "Sex": "Female",
    "AgeGroup": "55 to 59",
    "FacilityName": "The Mater Misericordiae Hospital",
    "PartnerName": "Coptic Orthodox Church",
    "Agency": "CDC",
    "Latitude": "-1.3067000000",
    "Longitude": "36.8343000000",
    "Lat": -1.3067000000,
    "Long": 36.8343000000,
    "Rate": 0.0
  },
  {
    "Indicator": "HIV POSITIVE NOT LINKED",
    "Numerator": 0,
    "Denominator": 1,
    "AssessmentPeriod": "2025-04-16T00:00:00",
    "County": "Nairobi",
    "SubCounty": "Starehe",
    "Sex": "Female",
    "AgeGroup": "35 to 39",
    "FacilityName": "The Mater Misericordiae Hospital",
    "PartnerName": "Coptic Orthodox Church",
    "Agency": "CDC",
    "Latitude": "-1.3067000000",
    "Longitude": "36.8343000000",
    "Lat": -1.3067000000,
    "Long": 36.8343000000,
    "Rate": 0.0
  },
  {
    "Indicator": "HIV POSITIVE NOT LINKED",
    "Numerator": 0,
    "Denominator": 1,
    "AssessmentPeriod": "2025-04-23T00:00:00",
    "County": "Nairobi",
    "SubCounty": "Starehe",
    "Sex": "Female",
    "AgeGroup": "35 to 39",
    "FacilityName": "The Mater Misericordiae Hospital",
    "PartnerName": "Coptic Orthodox Church",
    "Agency": "CDC",
    "Latitude": "-1.3067000000",
    "Longitude": "36.8343000000",
    "Lat": -1.3067000000,
    "Long": 36.8343000000,
    "Rate": 0.0
  },
  {
    "Indicator": "HIV POSITIVE NOT LINKED",
    "Numerator": 1,
    "Denominator": 1,
    "AssessmentPeriod": "2025-04-07T00:00:00",
    "County": "Nairobi",
    "SubCounty": "Starehe",
    "Sex": "Male",
    "AgeGroup": "35 to 39",
    "FacilityName": "The Mater Misericordiae Hospital",
    "PartnerName": "Coptic Orthodox Church",
    "Agency": "CDC",
    "Latitude": "-1.3067000000",
    "Longitude": "36.8343000000",
    "Lat": -1.3067000000,
    "Long": 36.8343000000,
    "Rate": 100.0
  },
  {
    "Indicator": "HIV POSITIVE NOT LINKED",
    "Numerator": 1,
    "Denominator": 1,
    "AssessmentPeriod": "2025-04-10T00:00:00",
    "County": "Nairobi",
    "SubCounty": "Starehe",
    "Sex": "Male",
    "AgeGroup": "45 to 49",
    "FacilityName": "The Mater Misericordiae Hospital",
    "PartnerName": "Coptic Orthodox Church",
    "Agency": "CDC",
    "Latitude": "-1.3067000000",
    "Longitude": "36.8343000000",
    "Lat": -1.3067000000,
    "Long": 36.8343000000,
    "Rate": 100.0
  }
]
     */
}