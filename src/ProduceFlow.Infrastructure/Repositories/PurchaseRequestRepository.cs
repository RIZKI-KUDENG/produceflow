using Microsoft.EntityFrameworkCore;
using ProduceFlow.Application.Interfaces;
using ProduceFlow.Domain.Entities;
using ProduceFlow.Infrastructure.Data;

namespace ProduceFlow.Infrastructure.Repositories;

public class PurchaseRequestRepository : IPurchaseRequestRepository
{
    private readonly AppDbContext _context;
    public PurchaseRequestRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<PurchaseRequest> AddAsync(PurchaseRequest purchaseRequest)
    {
        _context.PurchaseRequests.Add(purchaseRequest);
        await _context.SaveChangesAsync();
        return purchaseRequest;
    }
    public async Task<IEnumerable<PurchaseRequest>> GetAllAsync()
    {
        return await _context.PurchaseRequests
        .Include(pr => pr.Requester)
        .Include(pr => pr.Items)
        .ToListAsync();
    }
    public async Task<PurchaseRequest?> GetByIdAsync(int id)
    {
        return await _context.PurchaseRequests
        .Include(pr => pr.Requester)
        .Include(pr => pr.Items)
        .FirstOrDefaultAsync(pr => pr.Id == id);
    }
    public async Task UpdateAsync(PurchaseRequest purchaseRequest)
    {
        _context.PurchaseRequests.Update(purchaseRequest);
        await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(int id)
    {
        var purchaseRequest = await _context.PurchaseRequests.FindAsync(id);
        if (purchaseRequest != null)
        {
            _context.PurchaseRequests.Remove(purchaseRequest);
            await _context.SaveChangesAsync();
        }
    }
}