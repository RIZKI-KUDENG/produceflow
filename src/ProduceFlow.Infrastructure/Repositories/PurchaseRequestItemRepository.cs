using Microsoft.EntityFrameworkCore;
using ProduceFlow.Application.Interfaces;
using ProduceFlow.Domain.Entities;
using ProduceFlow.Infrastructure.Data;

namespace ProduceFlow.Infrastructure.Repositories;

public class PurchaserequestItemRepository : IPurchaseRequestItemRepository
{
    private readonly AppDbContext _context;

    public PurchaserequestItemRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PurchaseRequestItem> AddAsync(PurchaseRequestItem purchaseRequestItem)
    {
        _context.PurchaseRequestItems.Add(purchaseRequestItem);
        await _context.SaveChangesAsync();
        return purchaseRequestItem;
    }

    public async Task DeleteAsync(int id)
    {
        var item = await _context.PurchaseRequestItems.FindAsync(id);
        if (item != null)
        {
            _context.PurchaseRequestItems.Remove(item);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<PurchaseRequestItem>> GetAllAsync()
    {
        return await _context.PurchaseRequestItems.Include(pri => pri.Vendor)
                                                 .Include(pri => pri.PurchaseRequest)
                                                 .ToListAsync();
    }

    public async Task<PurchaseRequestItem?> GetByIdAsync(int id)
    {
        return await _context.PurchaseRequestItems.FindAsync(id);
    }

    public async Task UpdateAsync(PurchaseRequestItem purchaseRequestItem)
    {
        _context.PurchaseRequestItems.Update(purchaseRequestItem);
        await _context.SaveChangesAsync();
    }
}