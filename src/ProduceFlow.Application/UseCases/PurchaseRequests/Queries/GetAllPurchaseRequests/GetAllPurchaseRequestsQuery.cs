using MediatR;
using ProduceFlow.Application.Interfaces;
using ProduceFlow.Domain.Entities;

namespace ProduceFlow.Application.UseCases.PurchaseRequests.Queries.GetAllPurchaseRequests;

public record GetAllPurchaseRequestsQuery : IRequest<IEnumerable<PurchaseRequest>>;

public class GetAllPurchaseRequestsQueryHandler : IRequestHandler<GetAllPurchaseRequestsQuery, IEnumerable<PurchaseRequest>>
{
    private readonly IPurchaseRequestRepository _purchaseRequestRepository;

    public GetAllPurchaseRequestsQueryHandler(IPurchaseRequestRepository purchaseRequestRepository)
    {
        _purchaseRequestRepository = purchaseRequestRepository;
    }

    public async Task<IEnumerable<PurchaseRequest>> Handle(GetAllPurchaseRequestsQuery request, CancellationToken cancellationToken)
    {
        return await _purchaseRequestRepository.GetAllAsync();
    }
}