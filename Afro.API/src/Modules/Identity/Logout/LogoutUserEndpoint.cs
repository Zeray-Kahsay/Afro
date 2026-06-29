using Afro.API.src.BuildingBlocks.Results;

namespace Afro.API.src.Modules.Identity.Logout;

public static class LogoutUserEndpoint
{
    public static RouteGroupBuilder MapLogoutUser(this RouteGroupBuilder group)
    {
        group.MapPost(
            "/logout",
            async (
                LogoutUserCommand command,
                LogoutUserHandler handler,
                CancellationToken ct) =>
            {
                var result = await handler.HandleLogoutAsync(command, ct);

                return result.ToApiResult();
            }
        );

        return group;
    }
}
