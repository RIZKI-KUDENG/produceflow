using MediatR;
using ProduceFlow.Application.Interfaces;
using ProduceFlow.Domain.Entities;

namespace ProduceFlow.Application.UseCases.PurchaseRequests.Commands.CreatePurchaseRequest;

public record CreatePurchaseRequestCommand(string RequestNumber, int RequesterId, DateTime RequestedDate, decimal TotalEstimatedCost, string Status, string Reason) : IRequest<PurchaseRequest>;

public class CreatePurchaseRequestCommandHandler : IRequestHandler<CreatePurchaseRequestCommand, PurchaseRequest>
{
    private readonly IPurchaseRequestRepository _purchaseRequestRepository;

    public CreatePurchaseRequestCommandHandler(IPurchaseRequestRepository purchaseRequestRepository)
    {
        _purchaseRequestRepository = purchaseRequestRepository;
    }

    public async Task<PurchaseRequest> Handle(CreatePurchaseRequestCommand request, CancellationToken cancellationToken)
    {
        var purchaseRequest = new PurchaseRequest
        {
            RequestNumber = request.RequestNumber,
            RequesterId = request.RequesterId,
            RequestDate = request.RequestedDate,
            TotalEstimatedCost = request.TotalEstimatedCost,
            Status = request.Status,
            Reason = request.Reason
        };

        await _purchaseRequestRepository.AddAsync(purchaseRequest);
        return purchaseRequest;
    }
}