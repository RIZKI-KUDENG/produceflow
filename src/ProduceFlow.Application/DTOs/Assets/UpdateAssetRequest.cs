using ProduceFlow.Domain.Entities;

namespace ProduceFlow.Application.DTOs.Assets;

public class UpdateAssetRequest
{ 
        public string Name {get; set;} = string.Empty;
        public int CategoryId {get; set;}
        public decimal PurchasePrice {get; set;}
        public DateTime PurchaseDate {get; set;} 
        public int LocationId {get; set;}
        public int CurrentHolderId {get; set;}
        public string Status {get; set;} = string.Empty;
        public string SerialNumber {get; set;} = string.Empty;
}