using Afro.API.src.BuildingBlocks.Results;
using Microsoft.AspNetCore.Authorization;

namespace Afro.API.src.Modules.Identity.GetCurrentUser;

public static class GetCurrentUserEndpoint
{
    public static RouteGroupBuilder MapGetCurrentUser(this RouteGroupBuilder group)
    {
        group.MapGet(
            "/me",
            [Authorize]
        async (
                GetCurrentUserHandler handler,
                CancellationToken ct
            ) =>
            {
                var result = await handler.HandleCurrentUserAsync(ct);

                return result.ToApiResult();
            }
        );

        return group;
    }
}
