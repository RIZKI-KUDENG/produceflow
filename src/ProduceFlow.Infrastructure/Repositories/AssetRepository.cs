using Microsoft.EntityFrameworkCore;
using ProduceFlow.Application.Interfaces;
using ProduceFlow.Domain.Entities;
using ProduceFlow.Infrastructure.Data;

namespace ProduceFlow.Infrastructure.Repositories;

public class AssetRepository : IAssetRepository
{
    private readonly AppDbContext _context;

    public AssetRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Asset>> GetAllAsync()
    {
        return await _context.Assets.Include(a => a.Category)
                                    .Include(a => a.Location)
                                    .Include(a => a.CurrentHolder)
                                    .ToListAsync();
    }

    public async Task<Asset?> GetByIdAsync(int id)
    {
        return await _context.Assets.FindAsync(id);
    }

    public async Task<Asset> AddAsync(Asset asset)
    {
        _context.Assets.Add(asset);
        await _context.SaveChangesAsync();
        return asset;
    }
    public async Task UpdateAsync(Asset asset)
    {
        _context.Assets.Update(asset);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var asset = await _context.Assets.FindAsync( id);
        if (asset != null)
        {
            _context.Assets.Remove(asset);
            await _context.SaveChangesAsync();
        }
    }
}