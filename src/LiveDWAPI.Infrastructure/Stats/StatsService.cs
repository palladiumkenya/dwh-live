using System.Net.Http.Json;
using LiveDWAPI.Application.Stats.Interfaces;
using LiveDWAPI.Domain.Common;
using LiveDWAPI.Domain.Stats;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Serilog;

namespace LiveDWAPI.Infrastructure.Stats;

public class StatsService:IStatsService
{
    private readonly IOptions<ServicesApiOptions> _options;

    public StatsService(IOptions<ServicesApiOptions> options)
    {
        _options = options;
    }

    public async Task<List<SiteReporting>?> LoadCurrent()
    {
        var sites = new List<SiteReporting>();
        
        using (HttpClient client = new HttpClient())
        {
            try
            {
                string url = _options.Value.NDW;

                // Make the GET request
                HttpResponseMessage response = await client.GetAsync(url);

                // Ensure the request was successful
                response.EnsureSuccessStatusCode();
                
                var dataAsString = await response.Content.ReadAsStringAsync();
                sites= JsonConvert.DeserializeObject<List<SiteReporting>>(dataAsString);
            }
            catch (HttpRequestException e)
            {
                Log.Error(e, $"{_options.Value.NDW} Request error:");
                throw;
            }
        }

        return sites;
    }
    
}