using ProduceFlow.Domain.Entities;
using ProduceFlow.Application.DTOs.Assets;
using ProduceFlow.Application.Interfaces;

namespace ProduceFlow.Application.Services;

public class AssetService
{
    private readonly IAssetRepository _repository;

    public AssetService(IAssetRepository repository)
    {
        _repository = repository;
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
        if (request.Price <= 0)
        {
            throw new ArgumentException("Price must be greater than 0");
        }

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
    
}