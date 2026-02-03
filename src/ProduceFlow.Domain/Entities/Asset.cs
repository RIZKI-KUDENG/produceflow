using ProduceFlow.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProduceFlow.Domain.Entities;
    public class Asset : BaseEntity
    {
        public string AssetTag { get; set; } = Guid.NewGuid().ToString();
        public string Name {get; set;} = string.Empty;
        public int CategoryId {get; set;}
        public virtual Category Category {get; set;} = null!;
        public decimal PurchasePrice {get; set;}
        public DateTime PurchaseDate {get; set;} 
        public int LocationId {get; set;}
        public virtual Location Location {get; set;} = null!;
        public int? CurrentHolderId {get; set;}
        [ForeignKey("CurrentHolderId")]
        public virtual User CurrentHolder {get; set;} = null!;
        public string Status {get; set;} = "Available";
        public string SerialNumber {get; set;} = string.Empty;

    }


