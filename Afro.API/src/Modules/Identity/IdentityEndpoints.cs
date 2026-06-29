using Afro.API.src.Modules.Identity.GetCurrentUser;
using Afro.API.src.Modules.Identity.LoginUser;
using Afro.API.src.Modules.Identity.Logout;
using Afro.API.src.Modules.Identity.RefreshAccessToken;
using Afro.API.src.Modules.Identity.RegisterUser;

namespace Afro.API.src.Modules.Identity;

public static class IdentityEndpoints
{
    public static IEndpointRouteBuilder MapIdentityEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/auth");

        group.MapRegisterUser();
        group.MapLoginUser();
        group.MapRefreshAccesToken();
        group.MapLogoutUser();
        group.MapGetCurrentUser();

        return app;
    }
}
