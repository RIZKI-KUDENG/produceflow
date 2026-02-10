using MediatR;
using ProduceFlow.Application.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace ProduceFlow.Application.UseCases.Assets.Commands.DeleteAsset;

public record DeleteAssetCommand(int Id) : IRequest<bool>;

public class DeleteAssetCommandHandler : IRequestHandler<DeleteAssetCommand, bool>
{
    private readonly IAssetRepository _repository;
    private readonly IDistributedCache _cache;

    public DeleteAssetCommandHandler(IAssetRepository repository, IDistributedCache cache)
    {
        _repository = repository;
        _cache = cache;
    }

    public async Task<bool> Handle(DeleteAssetCommand request, CancellationToken cancellationToken)
    {
        var asset = await _repository.GetByIdAsync(request.Id);

        if (asset == null)
        {
            throw new KeyNotFoundException($"Asset dengan ID {request.Id} tidak ditemukan");
        }

        await _repository.DeleteAsync(request.Id);
        await _cache.RemoveAsync("Asset_List", cancellationToken);
        return true;
    }
}