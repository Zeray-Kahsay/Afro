using Afro.API.src.BuildingBlocks.Results;
using Afro.API.src.Modules.Identity.Constants;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Afro.API.src.Modules.Listings.Properties.UploadPropertyImages;

public static class UploadPropertyImagesEndpoint
{
    public static RouteGroupBuilder MapUploadPropertyImages(this RouteGroupBuilder group)
    {
        group.MapPost(
            "/properties/{propertyId:guid}/images",
            [Authorize(
                Roles = $"{RoleNames.Admin},{RoleNames.Agent}"
            )]
        async (
                    Guid propertyId,
                    UploadPropertyImagesCommand command,
                    [FromServices] UploadPropertyImagesValidator validator,
                    [FromServices] UploadPropertyImagesHandler handler,
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
