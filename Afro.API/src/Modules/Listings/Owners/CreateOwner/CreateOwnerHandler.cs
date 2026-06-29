using Afro.API.src.BuildingBlocks.Results;
using Afro.API.src.Infrastructure.Persistence;
using Afro.API.src.Modules.Listings.Constants;
using Afro.API.src.Modules.Listings.Domain;
using Microsoft.EntityFrameworkCore;

namespace Afro.API.src.Modules.Listings.Owners.CreateOwner;

public sealed class CreateOwnerHandler(AppDbContext dbContext)
{
    public async Task<Result<OwnerResponse>> HandleCreateOwnerAsync(
        CreateOwnerCommand command,
        CancellationToken ct)
    {
        var exists = await dbContext.Owners.AnyAsync(
             ow => ow.PhoneNumber == command.PhoneNumber, ct);

        if (exists)
        {
            return Result<OwnerResponse>.Failure(
                ListingErrors.OwnerPhoneAlreadyExists);
        }

        var owner = Owner.Create(
            command.FullName,
            command.PhoneNumber,
            command.Email,
            command.Address,
            command.Notes);

        dbContext.Owners.Add(owner);

        await dbContext.SaveChangesAsync(ct);

        return Result<OwnerResponse>.Success(
            new OwnerResponse(
                owner.Id,
                owner.FullName,
                owner.PhoneNumber,
                owner.Email,
                owner.Address
            ));
    }
}
