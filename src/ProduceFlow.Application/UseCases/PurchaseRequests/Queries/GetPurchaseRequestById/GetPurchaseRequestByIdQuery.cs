using MediatR;
using ProduceFlow.Application.Interfaces;
using ProduceFlow.Domain.Entities;

namespace ProduceFlow.Application.UseCases.PurchaseRequests.Queries.GetPurchaseRequestById;

public record GetPurchaseRequestByIdQuery(int Id) : IRequest<PurchaseRequest?>;

public class GetPurchaseRequestByIdQueryHandler : IRequestHandler<GetPurchaseRequestByIdQuery, PurchaseRequest?>
{
    private readonly IPurchaseRequestRepository _purchaseRequestRepository;

    public GetPurchaseRequestByIdQueryHandler(IPurchaseRequestRepository purchaseRequestRepository)
    {
        _purchaseRequestRepository = purchaseRequestRepository;
    }

    public async Task<PurchaseRequest?> Handle(GetPurchaseRequestByIdQuery request, CancellationToken cancellationToken)
    {
        return await _purchaseRequestRepository.GetByIdAsync(request.Id);
    }
}