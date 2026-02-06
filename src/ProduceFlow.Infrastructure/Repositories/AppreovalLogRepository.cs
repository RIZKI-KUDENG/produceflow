using Microsoft.EntityFrameworkCore;
using ProduceFlow.Application.Interfaces;
using ProduceFlow.Domain.Entities;
using ProduceFlow.Infrastructure.Data;

namespace ProduceFlow.Infrastructure.Repositories;

public class ApprovalLogRepository : IApprovalLogRepository
{
    private readonly AppDbContext _context;

    public ApprovalLogRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ApprovalLog> AddAsync(ApprovalLog approvalLog)
    {
        _context.ApprovalLogs.Add(approvalLog);
        await _context.SaveChangesAsync();
        return approvalLog;
    }
    public async Task DeleteAsync(int id)
    {
        var approvalLog = await _context.ApprovalLogs.FindAsync(id);
        if (approvalLog != null)
        {
            _context.ApprovalLogs.Remove(approvalLog);
            await _context.SaveChangesAsync();
        }
    }
    public async Task<IEnumerable<ApprovalLog>> GetAllAsync()
    {
        return await _context.ApprovalLogs.ToListAsync();
    }
    public async Task<ApprovalLog?> GetByIdAsync(int id)
    {
        return await _context.ApprovalLogs.FindAsync(id);
    }
    public async Task<ApprovalLog> UpdateAsync(ApprovalLog approvalLog)
    {
        _context.ApprovalLogs.Update(approvalLog);
        await _context.SaveChangesAsync();
        return approvalLog;
    }
}