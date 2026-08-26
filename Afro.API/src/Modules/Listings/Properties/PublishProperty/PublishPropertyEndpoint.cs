using Afro.API.src.BuildingBlocks.Results;
using Afro.API.src.Modules.Identity.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Afro.API.src.Modules.Listings.Properties.PublishProperty;

public static class PublishPropertyEndpoint
{
    public static RouteGroupBuilder MapPublishProperty(this RouteGroupBuilder group)
    {
        group.MapPost(
            "/properties/{propertyId:guid}/publish",
            [Authorize(
                Roles = $"{RoleNames.Admin},{RoleNames.Agent}"
            )]
        async (
                Guid propertyId,
                [FromServices] PublishPropertyHandler handler,
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
