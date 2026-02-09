using ProduceFlow.Domain.Entities;

namespace ProduceFlow.Application.DTOs.Assets;

public class AssetResponse
{
    public int Id { get; set; }
    public string? AssetTag { get; set; }
    public string? Name { get; set; }
    public string? Status { get; set; }

    public int? CategoryId { get; set; }

    public string? CategoryName { get; set; }
    public string? LocationName { get; set; }
    public int? LocationId { get; set; }
    public string? CurrentHolderName { get; set; }
    public int? CurrentHolderId { get; set; }

    public decimal PurchasePrice { get; set; }
    public DateTime PurchaseDate { get; set; }
}