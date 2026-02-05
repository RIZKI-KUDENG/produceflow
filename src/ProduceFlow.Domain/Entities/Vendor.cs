using ProduceFlow.Domain.Common;

namespace ProduceFlow.Domain.Entities;

public class Vendor : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public string ContactPerson { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public bool IsVerified { get; set; }
}