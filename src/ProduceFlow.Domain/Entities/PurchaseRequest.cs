using ProduceFlow.Domain.Common;

namespace ProduceFlow.Domain.Entities
{
    public class PurchaseRequest : BaseEntity
    {
        public string RequestNumber {get; set; } = string.Empty;
        public int RequesterId {get; set; }
        public DateTime RequestDate {get; set; }
        public decimal TotalEstimatedCost {get; set;  }
        public string Status {get; set; } = string.Empty;
        public string Reason {get; set; } = string.Empty;
    }
}