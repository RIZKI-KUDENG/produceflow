using Microsoft.Extensions.DependencyInjection;
using ProduceFlow.Application.Services;

namespace ProduceFlow.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<AssetService>();

        return services;
    }
}