using MediatR;
using ProduceFlow.Application.Interfaces;
using ProduceFlow.Domain.Entities;

namespace ProduceFlow.Application.UseCases.PurchaseRequestItems.Commands.UpdatePurchaseRequestItem;

public record UpdatePurchaseRequestItemCommand(
    int Id,
    string ItemName,
    string Specifications,
    int Quantity,
    decimal EstimatedCost
) : IRequest<PurchaseRequestItem>;

public class UpdatePurchaseRequestItemCommandHandler : IRequestHandler<UpdatePurchaseRequestItemCommand, PurchaseRequestItem>
{
    private readonly IPurchaseRequestItemRepository _purchaseRequestItemRepository;

    public UpdatePurchaseRequestItemCommandHandler(IPurchaseRequestItemRepository purchaseRequestItemRepository)
    {
        _purchaseRequestItemRepository = purchaseRequestItemRepository;
    }

    public async Task<PurchaseRequestItem> Handle(UpdatePurchaseRequestItemCommand request, CancellationToken cancellationToken)
    {
        var purchaseRequestItem = await _purchaseRequestItemRepository.GetByIdAsync(request.Id);

        if (purchaseRequestItem == null)
        {
            throw new KeyNotFoundException($"PurchaseRequestItem with Id {request.Id} not found.");
        }

        purchaseRequestItem.ItemName = request.ItemName;
        purchaseRequestItem.Specifications = request.Specifications;
        purchaseRequestItem.Quantity = request.Quantity;
        purchaseRequestItem.EstimatedCost = request.EstimatedCost;

        await _purchaseRequestItemRepository.UpdateAsync(purchaseRequestItem);

        return purchaseRequestItem;
    }
}