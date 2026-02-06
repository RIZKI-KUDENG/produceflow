using ProduceFlow.Domain.Entities;

namespace ProduceFlow.Application.Interfaces;
public interface IApprovalLogRepository
{
    Task<ApprovalLog> AddAsync(ApprovalLog approvalLog);
    Task<ApprovalLog?> GetByIdAsync(int id);
    Task<IEnumerable<ApprovalLog>> GetAllAsync();
    Task<ApprovalLog> UpdateAsync(ApprovalLog approvalLog);
    Task DeleteAsync(int id);
}