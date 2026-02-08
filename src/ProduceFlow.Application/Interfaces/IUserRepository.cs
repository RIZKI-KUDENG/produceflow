using ProduceFlow.Domain.Entities;
using ProduceFlow.Application.DTOs.Auth;

namespace ProduceFlow.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> IsEmailUniqueAsync(string email);
        Task AddAsync(User user);
        Task<User?> GetByEmailAsync(string email);

        Task<IEnumerable<UserResponse>> GetUsersAsync(string? search);

        Task UpdateAsync(User user);
    }
}