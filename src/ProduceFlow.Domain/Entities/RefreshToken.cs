using ProduceFlow.Domain.Common;

namespace ProduceFlow.Domain.Entities;

public class RefreshToken : BaseEntity
{
    public string Token { get; set; } = string.Empty;
    public DateTime Expires { get; set; }
    public bool IsExpired => DateTime.UtcNow >= Expires;
    
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public string? CreatedByIp { get; set; }
    
    public DateTime? Revoked { get; set; }
    public string? RevokedByIp { get; set; }
    public string? ReasonRevoked { get; set; }
    public bool IsActive => Revoked == null && !IsExpired;

    public int UserId { get; set; }
    public User User { get; set; } = null!;
}