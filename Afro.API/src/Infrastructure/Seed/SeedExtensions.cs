using Afro.API.src.Modules.Identity.Domain;
using Microsoft.AspNetCore.Identity;

namespace Afro.API.src.Infrastructure.Seed;

public static class SeedExtensions
{
    public static async Task SeedDatabaseAsync(this IServiceProvider services)
    {
        using var scope = services.CreateScope();

        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

        await RoleSeeder.SeedRolesAsync(roleManager);
        await AdminUserSeeder.SeedAdminAsync(userManager);
        
    }
}
