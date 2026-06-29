using Afro.API.src.Modules.Identity.Constants;
using Microsoft.AspNetCore.Authorization;

namespace Afro.API.src.Modules.Listings.Properties.SearchProperties;

public static class SearchPropertiesEndpoint
{
    public static RouteGroupBuilder MapSearchProperties(this RouteGroupBuilder group)
    {
        group.MapGet(
            "/properties",
            [Authorize(
                Roles = $"{RoleNames.Admin}, {RoleNames.Agent}"
            )]
            async (
                [AsParameters]
                SearchPropertiesQuery query,
                SearchPropertiesHandler handler,
                CancellationToken ct
            ) =>
            {
                var result = await handler.HandleAsync(query, ct);

                return Results.Ok(result);
            }
        );

        return group;
    }
}
