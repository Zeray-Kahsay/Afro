using Afro.API.src.BuildingBlocks.Results;

namespace Afro.API.src.Modules.Identity.RefreshAccessToken;

public static class RefreshAccessTokenEndpoint
{
    public static RouteGroupBuilder MapRefreshAccesToken(this RouteGroupBuilder group)
    {
        group.MapPost(
            "/refresh",
            async(
                RefreshAccessTokenCommand command,
                RefreshAccessTokenHandler handler,
                CancellationToken ct) =>
            {
                var result = await handler.HandleRefreshAccessTokenAsync(command, ct);

                return result.ToApiResult();
                
            }
        );

        return group;
    }
}
