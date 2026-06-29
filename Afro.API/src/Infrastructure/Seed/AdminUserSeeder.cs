using Afro.API.src.Modules.Identity.Constants;
using Afro.API.src.Modules.Identity.Domain;
using Microsoft.AspNetCore.Identity;

namespace Afro.API.src.Infrastructure.Seed;

public static class AdminUserSeeder
{
    public static async Task SeedAdminAsync(UserManager<AppUser> userManager)
    {
        // For dev purposes 

        // NB: In prod: Environment variable or Azure Key Vault 
        const string adminPhoneNumber = "+4799999999";

        const string adminPassword = "Admin@2026";

        var existingAdmin = await userManager.FindByNameAsync(adminPhoneNumber);

        if (existingAdmin is not null)
        {
            return;
        }

        var adminUser = AppUser.Create(adminPhoneNumber);

        var result = await userManager.CreateAsync(adminUser, adminPassword);

        if (!result.Succeeded)
        {
            throw new InvalidOperationException($"Failed to create admin user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }

        await userManager.AddToRoleAsync(adminUser, RoleNames.Admin);
    }
}
