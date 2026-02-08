using MediatR;
using ProduceFlow.Domain.Entities;
using ProduceFlow.Application.Interfaces;
using ProduceFlow.Application.DTOs.Assets;

namespace ProduceFlow.Application.UseCases.Assets.Queries.GetAllAssets;

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
