using Afro.API.src.Modules.Identity.Constants;
using Afro.API.src.BuildingBlocks.Results;
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
                [AsParameters] SearchOwnerRequest query,
                SearchOwnerHandler handler,
                CancellationToken ct
            ) =>
            {
                var result = await handler.HandleSearchOwnerAsync(query, ct);

                return result.ToApiResult();
            }
        );

        return group;
    }
}
