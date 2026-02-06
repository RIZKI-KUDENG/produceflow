using MediatR;
using ProduceFlow.Application.Interfaces;
using ProduceFlow.Domain.Entities;

namespace ProduceFlow.Application.UseCases.PurchaseRequestItems.Queries.GetAllPurchaseRequestItems;

public record GetAllPurchaseRequestItemsQuery : IRequest<IEnumerable<PurchaseRequestItem>>;

public class GetAllPurchaseRequestItemsQueryHandler : IRequestHandler<GetAllPurchaseRequestItemsQuery, IEnumerable<PurchaseRequestItem>>
{
    private readonly IPurchaseRequestItemRepository _purchaseRequestItemRepository;

    public GetAllPurchaseRequestItemsQueryHandler(IPurchaseRequestItemRepository purchaseRequestItemRepository)
    {
        _purchaseRequestItemRepository = purchaseRequestItemRepository;
    }

    public async Task<IEnumerable<PurchaseRequestItem>> Handle(GetAllPurchaseRequestItemsQuery request, CancellationToken cancellationToken)
    {
        return await _purchaseRequestItemRepository.GetAllAsync();
    }
}