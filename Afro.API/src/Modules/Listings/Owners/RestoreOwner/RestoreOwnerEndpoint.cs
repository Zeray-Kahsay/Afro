using Afro.API.src.BuildingBlocks.Results;
using Afro.API.src.Modules.Identity.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Afro.API.src.Modules.Listings.Owners.RestoreOwner;

public static class RestoreOwnerEndpoint
{
    public static RouteGroupBuilder MapRestoreOwner(this RouteGroupBuilder group)
    {
        group.MapPost(
            "/owners/{ownerId:guid}/restore",
            [Authorize(
                Roles = $"{RoleNames.Admin}, {RoleNames.Agent}"
            )]
        async (
                Guid ownerId,
                [FromServices] RestoreOwnerHandler handler,
                CancellationToken ct
            ) =>
            {
                var command = new RestoreOwnerCommand(ownerId);
                
                var result = await handler.HandleAsync(command, ct);

                return result.ToApiResult();
            }

        );

        return group;
    }
}
