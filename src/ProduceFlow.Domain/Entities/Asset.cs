using ProduceFlow.Domain.Common;

namespace ProduceFlow.Domain.Entities;
    public class Asset : BaseEntity
    {
        public string Name { get; set;} = string.Empty;
        public string? Description { get; set;}
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public AssetStatus Status { get; set; } = AssetStatus.Avalaible;
    }

    public enum AssetStatus
{
    Avalaible,
    Reserved,
    Sold,
    Broken
}
