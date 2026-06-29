using Afro.API.src.BuildingBlocks.Results;
using Afro.API.src.Modules.Identity.Constants;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Afro.API.src.Modules.Listings.Properties.UpdateProperty;

public static class UpdatePropertyEndpoint
{
    public static RouteGroupBuilder MapUpdateProperty(this RouteGroupBuilder group)
    {
        group.MapPut(
            "/properties/{propertyId:guid}",
            [Authorize(
                Roles = $"{RoleNames.Admin}, {RoleNames.Agent}"
            )]
        async (
                Guid propertyId,
                UpdatePropertyCommand command,
                [FromServices] UpdatePropertyValidator validator,
                [FromServices] UpdatePropertyHandler handler,
                CancellationToken ct
            ) =>
            {
                await validator.ValidateAndThrowAsync(command, ct);

                var result = await handler.HandleAsync(propertyId, command, ct);

                return result.ToApiResult();
            }
        );

        return group;
    }
}
