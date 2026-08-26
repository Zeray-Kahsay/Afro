using Afro.API.src.BuildingBlocks.Authentication;
using Afro.API.src.BuildingBlocks.Results;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Afro.API.src.Modules.Listings.Owners.ArchiveOwner;

public static class ArchiveOwnerEndpoint
{
    public static RouteGroupBuilder MapDeleteOwner(this RouteGroupBuilder group)
    {
        group.MapDelete(
            "/owners/{ownerId:guid}",
            [Authorize(Roles = $"{Policies.AppPolicies}")]

        async (
                Guid ownerId,
                [FromServices] ArchiveOwnerValidator validator,
                [FromServices] ArchiveOwnerHandler handler,
                CancellationToken ct
            ) =>
            {
               var command = new ArchiveOwnerCommand(ownerId);

                await validator.ValidateAndThrowAsync(command, ct);

                var result = await handler.HandleArchiveOwnerAsync(command, ct);

                return result.ToApiResult();
            }
        );

        return group;
    }

}
