using Microsoft.EntityFrameworkCore;

namespace Afro.API.src.Infrastructure.Persistence;

public static class DatabaseExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<AppDbContext>(
            options =>
            {
                options.UseSqlServer(
                    config.GetConnectionString("Database"));
            });
        
        return services;
    }
}
