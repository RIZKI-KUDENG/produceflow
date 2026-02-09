using Microsoft.EntityFrameworkCore;
using ProduceFlow.Application.Interfaces;
using ProduceFlow.Domain.Entities;
using ProduceFlow.Infrastructure.Data;
using ProduceFlow.Application.DTOs.Assets;

namespace ProduceFlow.Infrastructure.Repositories;

public class AssetRepository : IAssetRepository
{
    private readonly AppDbContext _context;

    public AssetRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AssetResponse>> GetAllAsync()
    {
        return await _context.Assets.Select(
            a => new AssetResponse
            {
                 Id = a.Id,
            AssetTag = a.AssetTag,
            Name = a.Name,
            Status = a.Status,

            CategoryName = a.Category.Name,
            CategoryId= a.CategoryId,
            LocationName = a.Location.Name,
            LocationId = a.LocationId,
            CurrentHolderName = a.CurrentHolder != null 
                ? a.CurrentHolder.FullName 
                : null,
            CurrentHolderId = a.CurrentHolderId != null ? a.CurrentHolderId.Value : null,

            PurchasePrice = a.PurchasePrice,
            PurchaseDate = a.PurchaseDate
            }
        ).ToListAsync();
    }

    public async Task<Asset?> GetByIdAsync(int id)
    {
        return await _context.Assets
        .Include(a => a.Category)
        .Include(a => a.Location)
        .Include(a => a.CurrentHolder)
        .FirstOrDefaultAsync(a => a.Id == id);
    }
    public async Task<AssetResponse?> GetDetailsByIdAsync(int id)
    {
        return await _context.Assets
        .Where(a => a.Id == id)
        .Select(a => new AssetResponse
        {
            Id = a.Id,
            AssetTag = a.AssetTag,
            Name = a.Name,
            Status = a.Status,

            CategoryName = a.Category.Name,
            CategoryId= a.CategoryId,
            LocationName = a.Location.Name,
            LocationId = a.LocationId,
            CurrentHolderName = a.CurrentHolder != null 
                ? a.CurrentHolder.FullName 
                : null,
            CurrentHolderId = a.CurrentHolderId != null ? a.CurrentHolderId.Value : 0,

            PurchasePrice = a.PurchasePrice,
            PurchaseDate = a.PurchaseDate
        })
        .FirstOrDefaultAsync();
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