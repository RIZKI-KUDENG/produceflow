using MediatR;
using ProduceFlow.Application.Interfaces;
using ProduceFlow.Domain.Entities;

namespace ProduceFlow.Application.UseCases.ApprovalLogs.Commands.DeleteApprovalLog;

public record DeleteApprovalLogCommand(int Id) : IRequest<bool>;

public class DeleteApprovalLogCommandHandler : IRequestHandler<DeleteApprovalLogCommand, bool>
{
    private readonly IApprovalLogRepository _approvalLogRepository;

    public DeleteApprovalLogCommandHandler(IApprovalLogRepository approvalLogRepository)
    {
        _approvalLogRepository = approvalLogRepository;
    }

    public async Task<bool> Handle(DeleteApprovalLogCommand request, CancellationToken cancellationToken)
    {
        var existingLog = await _approvalLogRepository.GetByIdAsync(request.Id);
        if (existingLog == null)
        {
            throw new KeyNotFoundException($"ApprovalLog with Id {request.Id} not found.");
        }

        await _approvalLogRepository.DeleteAsync(request.Id);
        return true; 
    }
}