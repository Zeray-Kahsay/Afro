using Afro.API.src.BuildingBlocks.Results;
using Afro.API.src.Infrastructure.Persistence;
using Afro.API.src.Infrastructure.Queries;
using Afro.API.src.Modules.Listings.Constants;
using Microsoft.EntityFrameworkCore;

namespace Afro.API.src.Modules.Listings.Owners.GetOwner;

public sealed class GetOwnerHandler (AppDbContext dbContext)
{
    public async Task<Result<OwnerResponse>> HandleGetOwnerAsync(Guid ownerId, CancellationToken ct)
    {
        var owner = await dbContext.GetActiveByIdAsync(ownerId, ct);
     
        
        if (owner is null)
        {
            return Result<OwnerResponse>.Failure(ListingErrors.OwnerNotFound);
        }

        return Result<OwnerResponse>.Success(new OwnerResponse(
                owner.Id,
                owner.FullName,
                owner.PhoneNumber,
                owner.Email,
                owner.Address,
                owner.Notes
        ));

    }
}
