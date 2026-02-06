using MediatR;
using ProduceFlow.Application.Interfaces;
using ProduceFlow.Domain.Entities;

namespace ProduceFlow.Application.UseCases.ApprovalLogs.Queries.GetApprovalLogById;

public record GetApprovalLogByIdQuery(int Id) : IRequest<ApprovalLog?>;

public class GetApprovalLogByIdQueryHandler : IRequestHandler<GetApprovalLogByIdQuery, ApprovalLog?>
{
    private readonly IApprovalLogRepository _approvalLogRepository;

    public GetApprovalLogByIdQueryHandler(IApprovalLogRepository approvalLogRepository)
    {
        _approvalLogRepository = approvalLogRepository;
    }

    public async Task<ApprovalLog?> Handle(GetApprovalLogByIdQuery request, CancellationToken cancellationToken)
    {
        return await _approvalLogRepository.GetByIdAsync(request.Id);
    }
}