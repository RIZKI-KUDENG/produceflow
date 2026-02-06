using MediatR;
using ProduceFlow.Domain.Entities;
using ProduceFlow.Application.Interfaces;

namespace ProduceFlow.Application.UseCases.PurchaseRequestItems.Commands.CreatePurchaseRequestItem;

public record CreatePurchaseRequestItemCommand(
    int PurchaseRequestId,
    string ItemName,
    string Specifications,
    int Quantity,
    decimal EstimatedCost
    , int VendorId
) : IRequest<PurchaseRequestItem>;

public class CreatePurchaseRequestItemCommandHandler : IRequestHandler<CreatePurchaseRequestItemCommand, PurchaseRequestItem>
{
    private readonly IPurchaseRequestItemRepository _purchaseRequestItemRepository;

    public CreatePurchaseRequestItemCommandHandler(IPurchaseRequestItemRepository purchaseRequestItemRepository)
    {
        _purchaseRequestItemRepository = purchaseRequestItemRepository;
    }

    public async Task<PurchaseRequestItem> Handle(CreatePurchaseRequestItemCommand request, CancellationToken cancellationToken)
    {
        var purchaseRequestItem = new PurchaseRequestItem
        {
            PurchaseRequestId = request.PurchaseRequestId,
            ItemName = request.ItemName,
            Specifications = request.Specifications,
            Quantity = request.Quantity,
            EstimatedCost = request.EstimatedCost,
            VendorId = request.VendorId
        };

        return await _purchaseRequestItemRepository.AddAsync(purchaseRequestItem);
    }
}