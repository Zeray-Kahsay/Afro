using Afro.API.src.BuildingBlocks.Results;
using Afro.API.src.Modules.Identity.Constants;
using Microsoft.AspNetCore.Authorization;

namespace Afro.API.src.Modules.Listings.Properties.GetProperty;

public static class GetPropertyEndpoint
{
    public static RouteGroupBuilder MapGetProperty(this RouteGroupBuilder group)
    {
        group.MapGet(
            "/properties/{propertyId:guid}",
            [Authorize(
                Roles = $"{RoleNames.Admin}, {RoleNames.Agent}"
            )]
            async (
                Guid propertyId,
                GetPropertyHandler handler,
                CancellationToken ct
            ) =>
            {
                var result = await handler.HandleAsync(propertyId, ct);

                return result.ToApiResult();
            }
        );

        return group;
    }
}
