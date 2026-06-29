using Afro.API.src.Infrastructure.Persistence;
using Afro.API.src.Modules.Identity.Constants;
using Afro.API.src.Modules.Identity.Domain;
using Afro.API.src.Modules.Identity.GetCurrentUser;
using Afro.API.src.Modules.Identity.LoginUser;
using Afro.API.src.Modules.Identity.Logout;
using Afro.API.src.Modules.Identity.RefreshAccessToken;
using Afro.API.src.Modules.Identity.RegisterUser;
using Microsoft.AspNetCore.Identity;

namespace Afro.API.src.Modules.Identity;

public static class IdentityModule
{
    public static IServiceCollection AddIdentityModule(this IServiceCollection services)
    {

        services.AddIdentityCore<AppUser>()
            .AddRoles<AppRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();

        services.AddScoped<RegisterUserHandler>();
        services.AddScoped<RegisterUserValidator>();
        services.AddScoped<LoginUserHandler>();
        services.AddScoped<LoginUserValidator>();
        services.AddScoped<RefreshAccessTokenHandler>();
        services.AddScoped<LogoutUserHandler>();
        services.AddScoped<GetCurrentUserHandler>();

        return services;
    }
}
