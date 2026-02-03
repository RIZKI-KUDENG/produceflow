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
            CategoryId = request.CategoryId,
            PurchaseDate = request.PurchaseDate,
            PurchasePrice = request.PurchasePrice,
            LocationId = request.LocationId,
            CurrentHolder = request.CurrentHolder,
            Status = request.Status,
            SerialNumber = request.SerialNumber
            
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

 public async Task UpdateAssetAsync(int id, UpdateAssetRequest request)
{

    await _updateValidator.ValidateAndThrowAsync(request);


    var existingAsset = await _repository.GetByIdAsync(id);

    if (existingAsset is null)
    {

        throw new KeyNotFoundException($"Asset dengan ID {id} tidak ditemukan");
    }


    existingAsset.Name = request.Name;
    existingAsset.CategoryId = request.CategoryId;
    existingAsset.PurchaseDate = request.PurchaseDate;
    existingAsset.PurchasePrice = request.PurchasePrice;
    existingAsset.LocationId = request.LocationId;
    existingAsset.CurrentHolder = request.CurrentHolder;
    existingAsset.Status = request.Status;
    existingAsset.SerialNumber = request.SerialNumber;

    await _repository.UpdateAsync(existingAsset);
}

}