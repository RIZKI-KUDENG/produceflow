using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using ProduceFlow.Application.Common.Attributes;
using System.Text.Json;
using System.Reflection;

namespace ProduceFlow.Application.Common.Behaviors;

public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
where TRequest : IRequest<TResponse>
{
    private readonly IDistributedCache _cache;
    private readonly ILogger<CachingBehavior<TRequest, TResponse>> _logger;

    public CachingBehavior(IDistributedCache cache, ILogger<CachingBehavior<TRequest, TResponse>> logger)
    {
        _cache = cache;
        _logger = logger;
    }
    public async Task<TResponse> Handle(TRequest request,  RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var cachedAttribute = request.GetType().GetCustomAttribute<CachedAttribute>();
        if (cachedAttribute == null)
        {
            return await next();
        }
        var cacheKey = !string.IsNullOrEmpty(cachedAttribute.CustomKey)
            ? cachedAttribute.CustomKey
            : $"{request.GetType().Name}_{JsonSerializer.Serialize(request)}";
        try
        {
            var cachedData = await _cache.GetStringAsync(cacheKey, cancellationToken);
            if (!string.IsNullOrEmpty(cachedData))
            {
                _logger.LogInformation($"Cache HIT untuk key: {cacheKey}");
                return JsonSerializer.Deserialize<TResponse>(cachedData)!;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Redis Error saat GET. Lanjut ke Database.");
        }
        var response = await next();
        try
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(cachedAttribute.ExpiredMinutes)
            };
            
            var serializedData = JsonSerializer.Serialize(response);
            await _cache.SetStringAsync(cacheKey, serializedData, options, cancellationToken);
            _logger.LogInformation($"Cache SET untuk key: {cacheKey}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Redis Error saat SET.");
        }

        return response;
    }
}