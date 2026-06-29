using Afro.API.src.Modules.Identity.Constants;
using Microsoft.AspNetCore.Authorization;

namespace Afro.API.src.Modules.Listings.Owners.SearchOwners;

public static class SearchOwnerQueryEndpoint
{
    public static RouteGroupBuilder MapSearchOwners(this RouteGroupBuilder group)
    {
        group.MapGet(
            "/owners",
            [Authorize(
                Roles = $"{RoleNames.Admin}, {RoleNames.Agent}"
            )]
            async (
                string? search,
                SearchOwnerHandler handler,
                CancellationToken ct
            ) =>
            {
                var result = await handler.HandleSearchOwnerAsync(
                    new SearchOwnerQuery(search), ct);
                
                return Results.Ok(result);
            }
        );

        return group;
    }
}
