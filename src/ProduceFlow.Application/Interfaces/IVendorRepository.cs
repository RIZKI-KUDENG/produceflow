using ProduceFlow.Domain.Entities;

namespace ProduceFlow.Application.Interfaces;

public interface IVendorRepository
{
    Task<IEnumerable<Vendor>> GetAllAsync();
    Task<Vendor?> GetByIdAsync(int id);
    Task<Vendor> AddAsync(Vendor vendor);
    Task UpdateAsync(Vendor vendor);
    Task DeleteAsync(int id);
}