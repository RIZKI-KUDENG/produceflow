namespace ProduceFlow.Application.DTOs.PurchaseRequests;

public class CreatePurchaseRequest
{
    public string RequestNumber { get; set; } = string.Empty;
    public int RequesterId { get; set; }
    public DateTime RequestDate { get; set; }
    public decimal TotalEstimatedCost { get; set; }
    public string Status { get; set; } = string.Empty;
    public string Reason { get; set; } = string.Empty;
}