using MediatR;
using ProduceFlow.Application.Interfaces;
using ProduceFlow.Domain.Entities;

namespace ProduceFlow.Application.UseCases.PurchaseRequestItems.Commands.DeletePurchaseRequestItem;

public record DeletePurchaseRequestItemCommand(int Id) : IRequest<bool>;

public class DeletePurchaseRequestItemCommandHandler : IRequestHandler<DeletePurchaseRequestItemCommand, bool>
{
    private readonly IPurchaseRequestItemRepository _purchaseRequestItemRepository;

    public DeletePurchaseRequestItemCommandHandler(IPurchaseRequestItemRepository purchaseRequestItemRepository)
    {
        _purchaseRequestItemRepository = purchaseRequestItemRepository;
    }

    public async Task<bool> Handle(DeletePurchaseRequestItemCommand request, CancellationToken cancellationToken)
    {
        var existingItem = await _purchaseRequestItemRepository.GetByIdAsync(request.Id);
        if (existingItem == null)
        {
            throw new KeyNotFoundException($"PurchaseRequestItem with Id {request.Id} not found.");
        }

        await _purchaseRequestItemRepository.DeleteAsync(request.Id);
        return true; 
    }
}