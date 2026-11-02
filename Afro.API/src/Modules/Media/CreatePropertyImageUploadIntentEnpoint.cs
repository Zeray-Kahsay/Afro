using Afro.API.src.BuildingBlocks.Results;
using Afro.API.src.Modules.Identity.Constants;
using Microsoft.AspNetCore.Authorization;

namespace Afro.API.src.Modules.Media;

public static class CreatePropertyImageUploadIntentEndpoint
{
    public static IEndpointRouteBuilder MapCreatePropertyImageUploadIntent(this IEndpointRouteBuilder app)
    {
        app.MapPost(
            "/api/properties/{propertyId:guid}/images/upload-intents",
            [Authorize(
                Roles = $"{RoleNames.Admin}, {RoleNames.Agent}"
            )]
        async (
                Guid propertyId,
                CreatePropertyImageUploadIntentHandler handler,
                CancellationToken ct
            ) =>
            {
                var result = await handler.HandleAsync(propertyId, ct);

                return result.ToApiResult();
            }
        );

        return app;

    }
}
