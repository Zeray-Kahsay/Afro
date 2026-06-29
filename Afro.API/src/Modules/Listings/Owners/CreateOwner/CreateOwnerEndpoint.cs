using Afro.API.src.BuildingBlocks.Results;
using Afro.API.src.Modules.Identity.Constants;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Afro.API.src.Modules.Listings.Owners.CreateOwner;

public static class CreateOwnerEndpoint
{
    public static RouteGroupBuilder MapCreateOwner(this RouteGroupBuilder group)
    {
        group.MapPost(
            "/owners",
            [Authorize(
                Roles = $"{RoleNames.Admin}, {RoleNames.Agent}"
            )]
        async (
                CreateOwnerCommand command,
                [FromServices] CreateOwnerValidator validator,
                [FromServices] CreateOwnerHandler handler,
                CancellationToken ct
            ) =>
            {
                await validator.ValidateAndThrowAsync(command, ct);

                var result = await handler.HandleCreateOwnerAsync(command, ct);

                return result.ToApiResult();
            }

        );

        return group;
    }
}
