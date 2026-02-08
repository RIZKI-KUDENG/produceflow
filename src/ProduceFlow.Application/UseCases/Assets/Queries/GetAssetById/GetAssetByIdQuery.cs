using MediatR;
using ProduceFlow.Domain.Entities;
using ProduceFlow.Application.Interfaces;
using ProduceFlow.Application.DTOs.Assets;

namespace ProduceFlow.Application.UseCases.Assets.Queries.GetAssetById;

public record GetAssetByIdQuery(int Id) : IRequest<AssetResponse>;

public class GetAssetByIdQueryHandler : IRequestHandler<GetAssetByIdQuery, AssetResponse>
{
    private readonly IAssetRepository _repository;

    public GetAssetByIdQueryHandler(IAssetRepository repository)
    {
        _repository = repository;
    }

    public async Task<AssetResponse> Handle(GetAssetByIdQuery request, CancellationToken cancellationToken)
    {
        var asset = await _repository.GetDetailsByIdAsync(request.Id);

        if(asset == null)
        {
            throw new KeyNotFoundException($"Asset dengan ID {request.Id} tidak ditemukan");
        }

        return asset;
    }
}