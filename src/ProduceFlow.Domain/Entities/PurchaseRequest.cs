using ProduceFlow.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProduceFlow.Domain.Entities
{
    public class PurchaseRequest : BaseEntity
    {
        public string RequestNumber {get; set; } = string.Empty;
        public int RequesterId {get; set; }
        [ForeignKey("RequesterId")]
        public virtual User? Requester {get; set; }
        public DateTime RequestDate {get; set; }
        public decimal TotalEstimatedCost {get; set;  }
        public string Status {get; set; } = string.Empty;
        public string Reason {get; set; } = string.Empty;
    }
}