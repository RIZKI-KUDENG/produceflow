using MediatR;
using ProduceFlow.Application.Interfaces;

namespace ProduceFlow.Application.UseCases.Assets.Commands.DeleteAsset;

public record DeleteAssetCommand(int Id) : IRequest<bool>;

public class DeleteAssetCommandHandler : IRequestHandler<DeleteAssetCommand, bool>
{
    private readonly IAssetRepository _repository;

    public DeleteAssetCommandHandler(IAssetRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeleteAssetCommand request, CancellationToken cancellationToken)
    {
        var asset = await _repository.GetByIdAsync(request.Id);

        if (asset == null)
        {
            throw new KeyNotFoundException($"Asset dengan ID {request.Id} tidak ditemukan");
        }

        await _repository.DeleteAsync(request.Id);
        return true;
    }
}