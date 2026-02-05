using MediatR;
using ProduceFlow.Application.Interfaces;


namespace ProduceFlow.Application.UseCases.PurchaseRequests.Commands.DeletePurchaseRequest;
public record DeletePurchaseRequestCommand(int Id) : IRequest<bool>;

public class DeletePurchaseRequestCommandHandler : IRequestHandler<DeletePurchaseRequestCommand, bool>
{
    private readonly IPurchaseRequestRepository _purchaseRequestRepository;

    public DeletePurchaseRequestCommandHandler(IPurchaseRequestRepository purchaseRequestRepository)
    {
        _purchaseRequestRepository = purchaseRequestRepository;
    }

    public async Task<bool> Handle(DeletePurchaseRequestCommand request, CancellationToken cancellationToken)
    {
        var purchaseRequest = await _purchaseRequestRepository.GetByIdAsync(request.Id);
        if (purchaseRequest == null)
        {
            throw new KeyNotFoundException($"Purchase Request with Id {request.Id} not found.");
        }

        await _purchaseRequestRepository.DeleteAsync(request.Id);
        return true;
    }
}