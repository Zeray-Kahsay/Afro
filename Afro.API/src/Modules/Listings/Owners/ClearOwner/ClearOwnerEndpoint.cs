using Afro.API.src.BuildingBlocks.Results;
using Afro.API.src.Modules.Identity.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Afro.API.src.Modules.Listings.Owners.ClearOwner;

public static class ClearOwnerEndpoint
{
    public static RouteGroupBuilder MapClearOwner(this RouteGroupBuilder group)
    {
        group.MapDelete(
             "/owners/{ownerId:guid}/clear",
             [Authorize(
                 Roles = $"{RoleNames.Admin}, {RoleNames.Agent}"
             )]
        async (
                 Guid ownerId,
                 [FromServices] ClearOwnerHandler handler,
                 CancellationToken ct
             ) =>
             {
                 var command = new ClearOwnerCommand(ownerId);

                 var result = await handler.HandleAsync(command, ct);

                 return result.ToApiResult();
             }

         );

        return group;

    }
}
