using CSharpFunctionalExtensions;
using LiveDWAPI.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Serilog;

namespace LiveDWAPI.Application.Services;

public class CachingService:ICachingService
{
    private readonly IMemoryCache _cache;
    private readonly IMediator _mediator;
    private static readonly SemaphoreSlim _semaphore = new(1, 1);

    public CachingService(IMemoryCache cache, IMediator mediator)
    {
        _cache = cache;
        _mediator = mediator;
    }

    public async Task<object> LoadFromCache(string cacheKey, IRequest<Result<object>> request)
    {
        try
        {
            if (_cache.TryGetValue(cacheKey, out object cachedData))
            {
                Log.Information($"reading Cache {cacheKey}");
                return cachedData;
            }

            // Only one thread should fetch data to avoid cache stampede
            await _semaphore.WaitAsync();
            try
            {
                // Double-check in case another thread already populated the cache
                if (_cache.TryGetValue(cacheKey, out cachedData))
                {
                    Log.Information($"reading Cache {cacheKey}");
                    return cachedData;
                }

                Log.Information($"Cache miss {cacheKey}");
                
                // Calculate expiration for next Monday at 2 AM
                var expiration = GetNextWeeklyRefreshTime();
            
                var res = await _mediator.Send(request);

                if (res.IsSuccess)
                {
                    _cache.Set(cacheKey, res.Value, new MemoryCacheEntryOptions
                    {
                        AbsoluteExpiration = expiration
                    });
                    Log.Debug($"Cached + {cacheKey}");
                    return res.Value;
                }
                throw new Exception($"An error occured: {res.Error}");
            }
            finally
            {
                _semaphore.Release();
            }
        }
        catch (Exception e)
        {
            Log.Error(e, "Error loading");
            throw;
        }
    }
    
    private DateTimeOffset GetNextWeeklyRefreshTime()
    {
        var now = DateTime.Now;
        var daysUntilMonday = ((int)DayOfWeek.Monday - (int)now.DayOfWeek + 7) % 7;
        var nextMonday = now.AddDays(daysUntilMonday);

        if (daysUntilMonday == 0 && now.TimeOfDay < TimeSpan.FromHours(2))
        {
            return new DateTimeOffset(now.Date.AddHours(2));
        }
        return new DateTimeOffset(nextMonday.Date.AddHours(2));
    }
}