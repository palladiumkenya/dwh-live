using CSharpFunctionalExtensions;
using MediatR;

namespace LiveDWAPI.Application.Interfaces;

public interface ICachingService
{
    Task<object> LoadFromCache(string cacheKey,IRequest<Result<object>> request);
}