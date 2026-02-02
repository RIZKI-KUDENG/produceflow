using ProduceFlow.Domain.Entities;

namespace ProduceFlow.Application.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateAccessToken(User user);
    RefreshToken GenerateRefreshToken();
}