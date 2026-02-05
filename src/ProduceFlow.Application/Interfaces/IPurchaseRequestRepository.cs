using ProduceFlow.Domain.Entities;

namespace ProduceFlow.Application.Interfaces;

public interface IPurchaseRequestRepository
{
    Task<PurchaseRequest> AddAsync(PurchaseRequest purchaseRequest);
    Task<PurchaseRequest?> GetByIdAsync(int id);
    Task<IEnumerable<PurchaseRequest>> GetAllAsync();
    Task UpdateAsync(PurchaseRequest purchaseRequest);
    Task DeleteAsync(int id);
}