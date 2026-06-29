using Afro.API.src.BuildingBlocks.Results;
using Afro.API.src.Modules.Identity.Constants;
using Microsoft.AspNetCore.Authorization;

namespace Afro.API.src.Modules.Listings.Properties.SetPropertyCoverImage;

public static class SetPropertyCoverImageEndpoint
{
    public static RouteGroupBuilder MapSetPropertyCoverImage(this RouteGroupBuilder group)
    {
        group.MapPut(
            "/properties/{propertyId:guid}/cover-image",
            [Authorize(
                Roles = $"{RoleNames.Admin}, {RoleNames.Agent}"
            )]
            async (
                Guid propertyId,
                SetPropertyCoverImageCommand command,
                SetPropertyCoverImageHandler handler,
                CancellationToken ct
            ) =>
            {
                var result = await handler.HandleAsync(propertyId, command, ct);

                return result.ToApiResult();
            }
        );

        return group;
    }
}
