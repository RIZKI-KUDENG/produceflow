namespace ProduceFlow.Application.DTOs.ApprovalLogs;

public class CreateApprovalLogRequest
{
    public int PurchaseRequestId { get; set; }
    public int ApprovedById { get; set; }
    public string Action { get; set; } = string.Empty;
    public string Comments { get; set; } = string.Empty;
    public DateTime ActionDate { get; set; } = DateTime.UtcNow;
}