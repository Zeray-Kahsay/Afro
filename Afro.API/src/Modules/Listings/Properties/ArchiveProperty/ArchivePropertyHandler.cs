using Afro.API.Modules.BuildingBlocks.Results;
using Afro.API.src.Infrastructure.Persistence;
using Afro.API.src.Modules.Listings.Constants;
using Microsoft.EntityFrameworkCore;

namespace Afro.API.src.Modules.Listings.Properties.ArchiveProperty;

public sealed class ArchivePropertyHandler(AppDbContext dbContext)
{
    public async Task<Result> HandleAsync(Guid propertyId, CancellationToken ct)
    {
        var property = await dbContext.Properties
                    .FirstOrDefaultAsync(x => x.Id == propertyId, ct);
        
        if (property is null)
        {
            return Result.Failure(ListingErrors.PropertyNotFound);
        }

        if (property.Status == Domain.ListingStatus.Archived)
        {
            return Result.Failure(ListingErrors.PropertyAlreadyAchived);
        }

        property.Archive();

        await dbContext.SaveChangesAsync(ct);

        return Result.Success();
    }
}
