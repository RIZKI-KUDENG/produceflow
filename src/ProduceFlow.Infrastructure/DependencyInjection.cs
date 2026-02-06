using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProduceFlow.Application.Interfaces;
using ProduceFlow.Infrastructure.Data;
using ProduceFlow.Infrastructure.Repositories;
using ProduceFlow.Infrastructure.Services;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
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
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IDepartmentRepository, DepartmentRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<ILocationRepository, LocationRepository>();
        services.AddScoped<IPurchaseRequestRepository, PurchaseRequestRepository>();
        services.AddScoped<IApprovalLogRepository, ApprovalLogRepository>();
        services.AddScoped<IPurchaseRequestItemRepository, PurchaserequestItemRepository>();

        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.Configure<JwtSettings>(
        configuration.GetSection(JwtSettings.SectionName)
);

        return services;
    }
}