using ProduceFlow.Domain.Common;

namespace ProduceFlow.Domain.Entities;
    public class Asset : BaseEntity
    {
        public string AssetTag { get; set; } = Guid.NewGuid().ToString();
        public string Name {get; set;} = string.Empty;
        public int CategoryId {get; set;}
        public decimal PurchasePrice {get; set;}
        public DateTime PurchaseDate {get; set;} 
        public int LocationId {get; set;}
        public int CurrentHolder {get; set;}
        public string Status {get; set;} = string.Empty;
        public string SerialNumber {get; set;} = string.Empty;

    }


