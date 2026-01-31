using ProduceFlow.Domain.Entities;

namespace ProduceFlow.Application.DTOs.Assets;

public class CreateAssetRequest
{
    public string Name {get; set;} = string.Empty;
    public string? Description {get; set;}
    public decimal Price {get; set;}
    public int Quantity {get; set;}
    public AssetStatus Status {get; set;}
}