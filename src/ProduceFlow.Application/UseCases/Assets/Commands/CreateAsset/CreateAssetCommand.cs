using MediatR;
using ProduceFlow.Domain.Entities;
using ProduceFlow.Application.Interfaces;
using FluentValidation;
using ProduceFlow.Application.DTOs.Assets;
using Microsoft.Extensions.Caching.Distributed;

namespace ProduceFlow.Application.UseCases.Assets.Commands.CreateAsset;

public record CreateAssetCommand(string Name, int CategoryId, DateTime PurchaseDate, decimal PurchasePrice, int LocationId, int? CurrentHolderId, string Status, string SerialNumber) : IRequest<Asset>;

public class CreateAssetCommandHandler : IRequestHandler<CreateAssetCommand, Asset>
{
    private readonly IAssetRepository _repository;
    private readonly IValidator<CreateAssetRequest> _validator;
    private readonly IDistributedCache _cache;

    public CreateAssetCommandHandler(IAssetRepository repository, IValidator<CreateAssetRequest> validator, IDistributedCache cache)
    {
        _repository = repository;
        _validator = validator;
        _cache = cache;
    }

    public async Task<Asset> Handle(CreateAssetCommand request, CancellationToken cancellationToken)
    {
        var dto = new CreateAssetRequest
        {
            Name = request.Name,
            CategoryId = request.CategoryId,
            PurchaseDate = request.PurchaseDate,
            PurchasePrice = request.PurchasePrice,
            LocationId = request.LocationId,
            CurrentHolderId = request.CurrentHolderId,
            Status = request.Status,
            SerialNumber = request.SerialNumber
        };
        await _validator.ValidateAndThrowAsync(dto, cancellationToken);

        var newAsset = new Asset
        {
            Name = request.Name,
            CategoryId = request.CategoryId,
            PurchaseDate = request.PurchaseDate,
            PurchasePrice = request.PurchasePrice,
            LocationId = request.LocationId,
            CurrentHolderId = request.CurrentHolderId,
            Status = request.Status,
            SerialNumber = request.SerialNumber
        };

        var result = await _repository.AddAsync(newAsset);
        await _cache.RemoveAsync("Asset_List", cancellationToken);
        return result;
    }
}