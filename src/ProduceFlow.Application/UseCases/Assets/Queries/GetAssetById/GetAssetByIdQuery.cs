using MediatR;
using ProduceFlow.Domain.Entities;
using ProduceFlow.Application.Interfaces;

namespace ProduceFlow.Application.UseCases.Assets.Queries.GetAssetById;

public record GetAssetByIdQuery(int Id) : IRequest<Asset>;

public class GetAssetByIdQueryHandler : IRequestHandler<GetAssetByIdQuery, Asset>
{
    private readonly IAssetRepository _repository;

    public GetAssetByIdQueryHandler(IAssetRepository repository)
    {
        _repository = repository;
    }

    public async Task<Asset> Handle(GetAssetByIdQuery request, CancellationToken cancellationToken)
    {
        var asset = await _repository.GetByIdAsync(request.Id);

        if(asset == null)
        {
            throw new KeyNotFoundException($"Asset dengan ID {request.Id} tidak ditemukan");
        }

        return asset;
    }
}