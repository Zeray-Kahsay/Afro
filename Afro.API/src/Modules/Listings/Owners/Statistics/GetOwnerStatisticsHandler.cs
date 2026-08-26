using Afro.API.src.BuildingBlocks.Results;
using Afro.API.src.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Afro.API.src.Modules.Listings.Owners.Statistics;

public sealed class GetOwnerStatisticsHandler(AppDbContext context)
{
    public async Task<Result<OwnerStatisticsResponse>> HandleAsync(CancellationToken ct)
    {
        var total = await context.Owners.CountAsync(ct);

        var active = await context.Owners.CountAsync(x => !x.IsArchived, ct);

        var archived = await context.Owners.CountAsync(x => x.IsArchived, ct);

        return Result<OwnerStatisticsResponse>.Success(
            new OwnerStatisticsResponse(
                total, active, archived));
        
    }
}
