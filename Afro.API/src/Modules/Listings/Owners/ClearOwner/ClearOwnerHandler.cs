using Afro.API.Modules.BuildingBlocks.Results;
using Afro.API.src.Infrastructure.Persistence;
using Afro.API.src.Modules.Listings.Constants;
using Microsoft.EntityFrameworkCore;

namespace Afro.API.src.Modules.Listings.Owners.ClearOwner;

public sealed class ClearOwnerHandler(AppDbContext context)
{
    public async Task<Result> HandleAsync(ClearOwnerCommand command, CancellationToken ct)
    {
        var owner = await context.Owners.FirstOrDefaultAsync(x => x.Id == command.ownerId, ct);

        if (owner is null)
        {
            return Result.Failure(ListingErrors.OwnerNotFound);
        }

        if (!owner.IsArchived)
        {
            return Result.Failure(ListingErrors.OwnerNotArchived);
        }

        // check if the owner has any active listings 

        var hasProperties = await context.Properties.AnyAsync(x => x.OwnerId == owner.Id, ct);

        if (hasProperties)
        {
            return Result.Failure(ListingErrors.OwnerHasActiveListings);
        }

        // if the owner is archived and has no active listings, we can clear the owner

        context.Owners.Remove(owner);

        await context.SaveChangesAsync(ct);

        return Result.Success();
    }
}
