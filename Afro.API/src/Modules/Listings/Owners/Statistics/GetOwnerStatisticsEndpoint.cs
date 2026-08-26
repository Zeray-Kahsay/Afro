using Afro.API.src.BuildingBlocks.Results;
using Afro.API.src.Modules.Identity.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Afro.API.src.Modules.Listings.Owners.Statistics;

public static class GetOwnerStatisticsEndpoint
{
    public static RouteGroupBuilder MapGetOwnerStatistics(this RouteGroupBuilder group)
    {
        group.MapGet(
            "/owners/statistics",
            [Authorize(
                Roles = $"{RoleNames.Admin}, {RoleNames.Agent}"
            )]
        async (
                [FromServices] GetOwnerStatisticsHandler handler,
                CancellationToken ct
            ) =>
            {
                var result = await handler.HandleAsync(ct);

                return result.ToApiResult();
            }
        );

        return group;
    }
}
