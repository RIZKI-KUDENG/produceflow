using MediatR;
using ProduceFlow.Application.Interfaces;
using ProduceFlow.Domain.Entities;

namespace ProduceFlow.Application.UseCases.PurchaseRequests.Commands.UpdatePurchaseRequest;

public record UpdatePurchaseRequestCommand(int Id, decimal TotalEstimatedCost, string Status, string Reason) : IRequest<PurchaseRequest>;

public class UpdatePurchaseRequestCommandHandler : IRequestHandler<UpdatePurchaseRequestCommand, PurchaseRequest>
{
    private readonly IPurchaseRequestRepository _purchaseRequestRepository;

    public UpdatePurchaseRequestCommandHandler(IPurchaseRequestRepository purchaseRequestRepository)
    {
        _purchaseRequestRepository = purchaseRequestRepository;
    }

    public async Task<PurchaseRequest> Handle(UpdatePurchaseRequestCommand request, CancellationToken cancellationToken)
    {
        var purchaseRequest = await _purchaseRequestRepository.GetByIdAsync(request.Id);
        if (purchaseRequest == null)
        {
            throw new KeyNotFoundException($"Purchase Request with Id {request.Id} not found.");
        }

        purchaseRequest.TotalEstimatedCost = request.TotalEstimatedCost;
        purchaseRequest.Status = request.Status;
        purchaseRequest.Reason = request.Reason;

        await _purchaseRequestRepository.UpdateAsync(purchaseRequest);
        return purchaseRequest;
    }
}