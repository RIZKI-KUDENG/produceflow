using ProduceFlow.Domain.Entities;
namespace ProduceFlow.Application.Interfaces;

public interface IRoleRepository
{
    Task<Role> AddAsync(Role role);
    Task<Role?> GetByNameAsync(string name);
}