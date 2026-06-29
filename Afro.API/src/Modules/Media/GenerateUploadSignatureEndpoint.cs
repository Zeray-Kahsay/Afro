using Afro.API.src.BuildingBlocks.Results;
using Afro.API.src.Modules.Identity.Constants;
using Microsoft.AspNetCore.Authorization;

namespace Afro.API.src.Modules.Media;

public static class GenerateUploadSignatureEndpoint
{
    public static IEndpointRouteBuilder MapGenerateUploadSignature(this IEndpointRouteBuilder app)
    {
        app.MapPost(
            "/api/media/upload-signature",
            [Authorize(
                Roles = $"{RoleNames.Admin}, {RoleNames.Agent}"
            )]
        async (
                GenerateUploadSignatureHandler handler,
                CancellationToken ct
            ) =>
            {
                var result = await handler.HandleAsync(ct);

                return result.ToApiResult();
            }
        );

        return app;

    }
}
