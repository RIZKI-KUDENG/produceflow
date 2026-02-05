namespace ProduceFlow.Application.DTOs.PurchaseRequests;

public class UpdatePurchaseRequest 
{
    public decimal TotalEstimatedCost { get; set;  }
    public string Status { get; set; } = string.Empty;
    public string Reason { get; set; } = string.Empty;
}