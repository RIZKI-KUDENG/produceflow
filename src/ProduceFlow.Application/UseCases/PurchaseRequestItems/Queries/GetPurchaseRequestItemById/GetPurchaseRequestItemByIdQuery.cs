using MediatR;
using ProduceFlow.Application.Interfaces;
using ProduceFlow.Domain.Entities;

namespace ProduceFlow.Application.UseCases.PurchaseRequestItems.Queries.GetPurchaseRequestItemById;

public record GetPurchaseRequestItemByIdQuery(int Id) : IRequest<PurchaseRequestItem?>;
public class GetPurchaseRequestItemByIdQueryHandler : IRequestHandler<GetPurchaseRequestItemByIdQuery, PurchaseRequestItem?>
{
    private readonly IPurchaseRequestItemRepository _purchaseRequestItemRepository;

    public GetPurchaseRequestItemByIdQueryHandler(IPurchaseRequestItemRepository purchaseRequestItemRepository)
    {
        _purchaseRequestItemRepository = purchaseRequestItemRepository;
    }

    public async Task<PurchaseRequestItem?> Handle(GetPurchaseRequestItemByIdQuery request, CancellationToken cancellationToken)
    {
        return await _purchaseRequestItemRepository.GetByIdAsync(request.Id);
    }
}