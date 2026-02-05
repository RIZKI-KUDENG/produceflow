using ProduceFlow.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProduceFlow.Domain.Entities;

public class PurchaseRequestItem : BaseEntity
{
    public int PurchaseRequestId { get; set; }
    [ForeignKey("PurchaseRequestId")]
    public PurchaseRequest? PurchaseRequest { get; set; }
    public string ItemName { get; set; } = string.Empty;
    public string Specifications { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal EstimatedCost { get; set; }
    public int VendorId { get; set; }
    [ForeignKey("VendorId")]
    public Vendor? Vendor { get; set; }
}