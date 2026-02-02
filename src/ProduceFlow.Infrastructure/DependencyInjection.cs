using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProduceFlow.Application.Interfaces;
using ProduceFlow.Infrastructure.Data;
using ProduceFlow.Infrastructure.Repositories;
using ProduceFlow.Infrastructure.Services;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ProduceFlow.Infrastructure.Authentication;

namespace ProduceFlow.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<AppDbContext>(options => 
        options.UseNpgsql(connectionString)
        );

        services.AddScoped<IAssetRepository, AssetRepository>();

        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

        return services;
    }
}