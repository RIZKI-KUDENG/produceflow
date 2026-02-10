using MediatR;
using ProduceFlow.Domain.Entities;
using ProduceFlow.Application.Interfaces;
using ProduceFlow.Application.DTOs.Assets;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace ProduceFlow.Application.UseCases.Assets.Queries.GetAllAssets;

public record GetAllAssetsQuery : IRequest<IEnumerable<AssetResponse>>;

public class GetAllAssetsQueryHandler : IRequestHandler<GetAllAssetsQuery, IEnumerable<AssetResponse>>
{
    private readonly IAssetRepository _repository;
    private readonly IDistributedCache _chace;

    public GetAllAssetsQueryHandler(IAssetRepository repository, IDistributedCache chace)
    {
        _repository = repository;
        _chace = chace;
    }

    public async Task<IEnumerable<AssetResponse>> Handle(GetAllAssetsQuery request, CancellationToken cancellationToken)
    {
        string chaceKey = "assets_get_all";
        string? chachedAssets = await _chace.GetStringAsync(chaceKey, cancellationToken);
        if (!string.IsNullOrEmpty(chachedAssets))
        {
            return JsonSerializer.Deserialize<IEnumerable<AssetResponse>>(chachedAssets)!;
        }

        var assets = await _repository.GetAllAsync();

        var chaceOptions = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
        };
        string serializedAssets = JsonSerializer.Serialize(assets);
        await _chace.SetStringAsync(chaceKey, serializedAssets, chaceOptions, cancellationToken);

        return assets;
    }
}
