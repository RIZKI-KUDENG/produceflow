using ProduceFlow.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;


namespace ProduceFlow.Domain.Entities;

public class ApprovalLog : BaseEntity
{
    public int PurchaseRequestId { get; set; }
    [ForeignKey("PurchaseRequestId")]
    public PurchaseRequest? PurchaseRequest { get; set; }
    public int ApprovedById { get; set; }
    [ForeignKey("ApprovedById")]
    public User? ApprovedBy { get; set; }
    public string Action { get; set; } = string.Empty;
    public string Comments { get; set; } = string.Empty;
    public DateTime ActionDate { get; set; } = DateTime.UtcNow;
}