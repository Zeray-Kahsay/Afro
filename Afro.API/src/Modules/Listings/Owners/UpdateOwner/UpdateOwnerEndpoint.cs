using Afro.API.src.BuildingBlocks.Results;
using Afro.API.src.Modules.Identity.Constants;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Afro.API.src.Modules.Listings.Owners.UpdateOwner;

public static class UpdateOwnerEndpoint
{
    public static RouteGroupBuilder MapUpdateOwner(this RouteGroupBuilder group)
    {
        group.MapPut(
            "/owners/{ownerId:guid}",
            [Authorize(
                Roles = $"{RoleNames.Admin}, {RoleNames.Agent}"
            )] async (
                Guid ownerId,
                [FromBody] UpdateOwnerCommand command,
                [FromServices] UpdateOwnerValidator validator,
                [FromServices] UpdateOwnerHandler handler,
                CancellationToken ct
            ) =>
            {
                command = command with
                {
                    OwnerId = ownerId
                };

                await validator.ValidateAndThrowAsync(command, ct);

                var result = await handler.Handle(command, ct);

                return result.ToApiResult();
            }
        );

        return group;
    }
}
