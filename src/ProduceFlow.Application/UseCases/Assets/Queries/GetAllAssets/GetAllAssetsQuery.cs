using MediatR;
using ProduceFlow.Domain.Entities;
using ProduceFlow.Application.Interfaces;

namespace ProduceFlow.Application.UseCases.Assets.Queries.GetAllAssets;

public record GetAllAssetsQuery : IRequest<IEnumerable<Asset>>;

public class GetAllAssetsQueryHandler : IRequestHandler<GetAllAssetsQuery, IEnumerable<Asset>>
{
    private readonly IAssetRepository _repository;

    public GetAllAssetsQueryHandler(IAssetRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Asset>> Handle(GetAllAssetsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync();
    }
}
