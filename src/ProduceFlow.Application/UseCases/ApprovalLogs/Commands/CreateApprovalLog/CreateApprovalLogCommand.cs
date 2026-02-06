using MediatR;
using ProduceFlow.Application.Interfaces;
using ProduceFlow.Domain.Entities;


namespace ProduceFlow.Application.UseCases.ApprovalLogs.Commands.CreateApprovalLog;

public record CreateApprovalLogCommand(
    int PurchaseRequestId,
    int ApprovedById,
    string Action,
    string Comments,
    DateTime ActionDate
) : IRequest<ApprovalLog>;

public class CreateApprovalLogCommandHandler : IRequestHandler<CreateApprovalLogCommand, ApprovalLog>
{
    private readonly IApprovalLogRepository _approvalLogRepository;

    public CreateApprovalLogCommandHandler(IApprovalLogRepository approvalLogRepository)
    {
        _approvalLogRepository = approvalLogRepository;
    }

    public async Task<ApprovalLog> Handle(CreateApprovalLogCommand request, CancellationToken cancellationToken)
    {
        var approvalLog = new ApprovalLog
        {
            PurchaseRequestId = request.PurchaseRequestId,
            ApprovedById = request.ApprovedById,
            Action = request.Action,
            Comments = request.Comments,
            ActionDate = request.ActionDate
        };

        return await _approvalLogRepository.AddAsync(approvalLog);
    }
}
