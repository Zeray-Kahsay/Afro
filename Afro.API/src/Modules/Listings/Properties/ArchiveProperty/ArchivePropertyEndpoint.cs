using Afro.API.src.BuildingBlocks.Results;
using Afro.API.src.Modules.Identity.Constants;
using Microsoft.AspNetCore.Authorization;

namespace Afro.API.src.Modules.Listings.Properties.ArchiveProperty;

public static class ArchivePropertyEndpoint
{
    public static RouteGroupBuilder MapArchiveProperty(this RouteGroupBuilder group)
    {
        group.MapPost(
            "/properties/{propertyId:guid}/archive",
            [Authorize(
                Roles = $"{RoleNames.Admin}, {RoleNames.Agent}"
            )]
            async (
                Guid propertyId,
                ArchivePropertyHandler handler,
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
