using MediatR;
using ProduceFlow.Application.DTOs.Assets;
using ProduceFlow.Application.Interfaces;
using ProduceFlow.Application.Common.Attributes; 

namespace ProduceFlow.Application.UseCases.Assets.Queries.GetAllAssets;


    [Cached(5, CustomKey = "Asset_List") ]
public record GetAllAssetsQuery : IRequest<IEnumerable<AssetResponse>>;

public class GetAllAssetsQueryHandler : IRequestHandler<GetAllAssetsQuery, IEnumerable<AssetResponse>>
{
    private readonly IAssetRepository _repository;
    public GetAllAssetsQueryHandler(IAssetRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<AssetResponse>> Handle(GetAllAssetsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync();
    }
}