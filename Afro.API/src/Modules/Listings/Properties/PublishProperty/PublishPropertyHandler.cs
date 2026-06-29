using Afro.API.Modules.BuildingBlocks.Results;
using Afro.API.src.Infrastructure.Persistence;
using Afro.API.src.Modules.Listings.Constants;
using Microsoft.EntityFrameworkCore;

namespace Afro.API.src.Modules.Listings.Properties.PublishProperty;

public sealed class PublishPropertyHandler(AppDbContext dbContext)
{
    public async Task<Result> HandleAsync(Guid propertyId, CancellationToken ct)
    {
        var property = await dbContext.Properties
                .Include(x => x.Images)
                .FirstOrDefaultAsync(x => x.Id == propertyId, ct);
        
        if (property is null)
        {
            return Result.Failure(ListingErrors.PropertyNotFound);
        }

        property.Publish();

        await dbContext.SaveChangesAsync(ct);

        return Result.Success();
    }
}
