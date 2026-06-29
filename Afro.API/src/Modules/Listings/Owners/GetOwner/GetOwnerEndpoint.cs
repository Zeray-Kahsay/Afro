using Afro.API.src.BuildingBlocks.Results;
using Afro.API.src.Modules.Identity.Constants;
using Microsoft.AspNetCore.Authorization;

namespace Afro.API.src.Modules.Listings.Owners.GetOwner;

public static class GetOwnerEndpoint
{
    public static RouteGroupBuilder MapGetOwner(this RouteGroupBuilder group)
    {
        group.MapGet(
            "/owners/{ownerId:guid}",
            [Authorize(
                Roles = $"{RoleNames.Admin}, {RoleNames.Agent}"
            )]
            async (
                Guid ownerId,
                GetOwnerHandler handler,
                CancellationToken ct
            ) =>
            {
                var result = await handler.HandleGetOwnerAsync(ownerId, ct);

                return result.ToApiResult();
            }
        );

        return group;
    }
}
