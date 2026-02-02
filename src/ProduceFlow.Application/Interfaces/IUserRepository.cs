using ProduceFlow.Domain.Entities;

namespace ProduceFlow.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> IsEmailUniqueAsync(string email);
        Task AddAsync(User user);
        Task<User?> GetByEmailAsync(string email);
    }
}