using ProduceFlow.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using ProduceFlow.Domain.Entities;
using ProduceFlow.Infrastructure.Data;

namespace ProduceFlow.Infrastructure.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly AppDbContext _context;
    public RoleRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<Role> AddAsync(Role role)
    {
        _context.Roles.Add(role);
        await  _context.SaveChangesAsync();
        return role;
    }
}