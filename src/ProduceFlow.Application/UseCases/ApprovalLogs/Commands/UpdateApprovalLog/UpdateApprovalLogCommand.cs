using MediatR;
using ProduceFlow.Application.Interfaces;
using ProduceFlow.Domain.Entities;

namespace ProduceFlow.Application.UseCases.ApprovalLogs.Commands.UpdateApprovalLog;

public record UpdateApprovalLogCommand(
    int Id,
    string Action,
    string Comments
) : IRequest<ApprovalLog>;

public class UpdateApprovalLogCommandHandler : IRequestHandler<UpdateApprovalLogCommand, ApprovalLog>
{
    private readonly IApprovalLogRepository _approvalLogRepository;

    public UpdateApprovalLogCommandHandler(IApprovalLogRepository approvalLogRepository)
    {
        _approvalLogRepository = approvalLogRepository;
    }

    public async Task<ApprovalLog> Handle(UpdateApprovalLogCommand request, CancellationToken cancellationToken)
    {
        var existingLog = await _approvalLogRepository.GetByIdAsync(request.Id);
        if (existingLog == null)
        {
            throw new KeyNotFoundException($"ApprovalLog with Id {request.Id} not found.");
        }

        existingLog.Action = request.Action;
        existingLog.Comments = request.Comments;

        return await _approvalLogRepository.UpdateAsync(existingLog);
    }
}