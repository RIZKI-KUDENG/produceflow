using ProduceFlow.Domain.Entities;
namespace ProduceFlow.Application.Interfaces;

public interface IPurchaseRequestItemRepository
{
    Task<IEnumerable<PurchaseRequestItem>> GetAllAsync();
    Task<PurchaseRequestItem?> GetByIdAsync(int id);
    Task<PurchaseRequestItem> AddAsync(PurchaseRequestItem purchaseRequestItem);
    Task UpdateAsync(PurchaseRequestItem purchaseRequestItem);
    Task DeleteAsync(int id);
}
