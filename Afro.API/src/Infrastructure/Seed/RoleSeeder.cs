using Afro.API.src.Modules.Identity.Constants;
using Afro.API.src.Modules.Identity.Domain;
using Microsoft.AspNetCore.Identity;

namespace Afro.API.src.Infrastructure.Seed;

public static class RoleSeeder
{
    public static async Task SeedRolesAsync(RoleManager<AppRole> roleManager)
    {
        foreach(var roleName in new[]
        {
            RoleNames.Admin,
            RoleNames.Agent,
            RoleNames.Customer
        })
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new AppRole
                {
                    Name = roleName
                });
            }
        }
      
    }
}
