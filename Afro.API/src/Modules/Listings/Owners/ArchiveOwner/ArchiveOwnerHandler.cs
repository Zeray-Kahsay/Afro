using Afro.API.src.BuildingBlocks.Results;
using Afro.API.src.Infrastructure.Persistence;
using Afro.API.src.Modules.Listings.common;
using Afro.API.src.Modules.Listings.Constants;

namespace Afro.API.src.Modules.Listings.Owners.ArchiveOwner;

public sealed class ArchiveOwnerHandler(AppDbContext context)
{
    public async Task<Result<bool>> HandleArchiveOwnerAsync(
        ArchiveOwnerCommand command,
        CancellationToken ct
    )
    {
        var owner = await context.FindOwnerIncludingArchivedAsync(command.OwnerId, ct );
        
        if (owner is null)
        {
            return Result<bool>.Failure(ListingErrors.OwnerNotFound);
        }

        owner.Archive();

        await context.SaveChangesAsync(ct);

        return Result<bool>.Success(true);
    }
}
