using Afro.API.Modules.BuildingBlocks.Results;
using Afro.API.src.Infrastructure.Persistence;
using Afro.API.src.Modules.Listings.Constants;
using Microsoft.EntityFrameworkCore;

namespace Afro.API.src.Modules.Listings.Properties.SetPropertyCoverImage;

public sealed class SetPropertyCoverImageHandler(AppDbContext dbContext)
{
    public async Task<Result> HandleAsync(
        Guid propertyId,
        SetPropertyCoverImageCommand command,
        CancellationToken ct
    )
    {
        var property = await dbContext.Properties
                        .Include(x => x.Images)
                        .FirstOrDefaultAsync(x => x.Id == propertyId, ct);
        
        if (property is null)
        {
            return Result.Failure(ListingErrors.PropertyNotFound);
        }

        property.SetCoverImage(command.ImageId);

        await dbContext.SaveChangesAsync(ct);

        return Result.Success();
    }

}
