using ProduceFlow.Domain.Entities;
using ProduceFlow.Application.DTOs.Assets;
using ProduceFlow.Application.Interfaces;
using FluentValidation;

namespace ProduceFlow.Application.Services;

public class AssetService
{
    private readonly IAssetRepository _repository;
    private readonly IValidator<CreateAssetRequest> _createValidator;

    private readonly IValidator<UpdateAssetRequest> _updateValidator;



    public AssetService(IAssetRepository repository, IValidator<CreateAssetRequest> createValidator,
        IValidator<UpdateAssetRequest> updateValidator)
    {
        _repository = repository;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    public async Task<IEnumerable<Asset>> GetAllAssetsAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Asset?> GetAssetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<Asset> CreateAssetAsync(CreateAssetRequest request)
    {
        await _createValidator.ValidateAndThrowAsync(request);

        var newAsset = new Asset
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            Quantity = request.Quantity,
            Status = request.Status
        };

        return await _repository.AddAsync(newAsset);
    }

    public async Task DeleteAssetAsync(int id)
    {
        var existingAsset = await _repository.GetByIdAsync(id);

        if (existingAsset is null)
        {
            throw new Exception("Asset not found");
        }

        await _repository.DeleteAsync(id);
    }

    public async Task UpdateAssetAsync(Asset asset, UpdateAssetRequest request)
    {
        await _updateValidator.ValidateAndThrowAsync(request);

        var existingAsset = await _repository.GetByIdAsync(asset.Id);

        if (existingAsset is null)
        {
            throw new Exception("Asset not found");
        }

        existingAsset.Name = asset.Name;
        existingAsset.Description = asset.Description;
        existingAsset.Price = asset.Price;
        existingAsset.Quantity = asset.Quantity;
        existingAsset.Status = asset.Status;
        existingAsset.UpdatedAt = DateTime.UtcNow;

        await _repository.UpdateAsync(existingAsset);
    }

}