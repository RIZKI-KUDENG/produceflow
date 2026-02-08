using ProduceFlow.Domain.Entities;
using ProduceFlow.Application.DTOs.Assets;

namespace ProduceFlow.Application.Interfaces;

public interface IAssetRepository
{
    Task<IEnumerable<AssetResponse>> GetAllAsync();
    Task<Asset?> GetByIdAsync(int id);

    Task<AssetResponse?> GetDetailsByIdAsync(int id);
    Task<Asset> AddAsync(Asset asset);
    Task UpdateAsync(Asset asset);
    Task DeleteAsync(int id);
}