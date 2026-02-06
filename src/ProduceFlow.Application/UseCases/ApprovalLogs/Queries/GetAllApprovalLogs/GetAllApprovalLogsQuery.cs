using MediatR;
using ProduceFlow.Application.Interfaces;
using ProduceFlow.Domain.Entities;

namespace ProduceFlow.Application.UseCases.ApprovalLogs.Queries.GetAllApprovalLogs;

public record GetAllApprovalLogsQuery : IRequest<IEnumerable<ApprovalLog>>;

public class GetAllApprovalLogsQueryHandler : IRequestHandler<GetAllApprovalLogsQuery, IEnumerable<ApprovalLog>>
{
    private readonly IApprovalLogRepository _approvalLogRepository;

    public GetAllApprovalLogsQueryHandler(IApprovalLogRepository approvalLogRepository)
    {
        _approvalLogRepository = approvalLogRepository;
    }

    public async Task<IEnumerable<ApprovalLog>> Handle(GetAllApprovalLogsQuery request, CancellationToken cancellationToken)
    {
        return await _approvalLogRepository.GetAllAsync();
    }
}