using ProduceFlow.Domain.Entities;

namespace ProduceFlow.Application.Interfaces;

public interface IAssetRepository
{
    Task<IEnumerable<Asset>> GetAllAsync();
    Task<Asset?> GetByIdAsync(int id);
    Task<Asset> AddAsync(Asset asset);
    Task UpdateAsync(Asset asset);
    Task DeleteAsync(int id);
}