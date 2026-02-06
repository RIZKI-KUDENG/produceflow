namespace ProduceFlow.Application.DTOs.PurchaseRequestItems;

public class UpdatePurchaseRequestItem
{
    public string ItemName { get; set; } = string.Empty;
    public string Specifications { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal EstimatedCost { get; set; }
}