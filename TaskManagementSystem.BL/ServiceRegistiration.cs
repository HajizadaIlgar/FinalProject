using Microsoft.Extensions.DependencyInjection;
using TaskManagementSystem.BL.Services.Abstracts;
using TaskManagementSystem.BL.Services.Implements;
using TaskManagementSystem.CORE.Repositories;
using TaskManagementSystem.DAL.Repositories;

namespace TaskManagementSystem.BL;

public static class ServiceRegistiration
{
    public static IServiceCollection AddAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ServiceRegistiration));
        return services;
    }
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICategoryService, CategoryService>();

        return services;
    }
}
