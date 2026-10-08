using Afro.API.Modules.BuildingBlocks.Results;
using Afro.API.src.Infrastructure.Persistence;
using Afro.API.src.Modules.Listings.Constants;
using Microsoft.EntityFrameworkCore;

namespace Afro.API.src.Modules.Listings.Owners.RestoreOwner;

public sealed class RestoreOwnerHandler(AppDbContext context)
{
    public async Task<Result> HandleAsync(RestoreOwnerCommand command, CancellationToken ct)
    {
        var owner = await context.Owners.FirstOrDefaultAsync(x => x.Id == command.OwnerId, ct);

        if (owner is null)
        {
            return Result.Failure(ListingErrors.OwnerNotFound);
        }

        if (!owner.IsArchived)
        {
            return Result.Failure(ListingErrors.OwnerNotArchived);
        }

        owner.Restore();

        await context.SaveChangesAsync(ct);

        return Result.Success();
    }
}
