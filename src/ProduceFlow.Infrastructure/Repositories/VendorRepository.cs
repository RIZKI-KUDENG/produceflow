using Microsoft.EntityFrameworkCore;
using ProduceFlow.Application.Interfaces;
using ProduceFlow.Domain.Entities;
using ProduceFlow.Infrastructure.Data;

namespace ProduceFlow.Infrastructure.Repositories;

public class VendorRepository : IVendorRepository
{
    private readonly AppDbContext _context;
    public VendorRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Vendor>> GetAllAsync()
    {
        return await _context.Vendors.ToListAsync();
    }
    public async Task<Vendor?> GetByIdAsync(int id)
    {
        return await _context.Vendors.FindAsync(id);
    }
    public async Task<Vendor> AddAsync(Vendor vendor)
    {
        _context.Vendors.Add(vendor);
        await _context.SaveChangesAsync();
        return vendor;
    }
    public async Task UpdateAsync(Vendor vendor)
    {
        _context.Vendors.Update(vendor);
        await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(int id)
    {
        var vendor = await _context.Vendors.FindAsync(id);
        if (vendor != null)
        {
            _context.Vendors.Remove(vendor);
            await _context.SaveChangesAsync();
        }
    }
}