using Afro.API.src.BuildingBlocks.Results;
using Afro.API.src.Infrastructure.Persistence;
using Afro.API.src.Modules.Listings.Constants;
using Afro.API.src.Modules.Listings.Domain;
using Microsoft.EntityFrameworkCore;

namespace Afro.API.src.Modules.Listings.Properties.UpdateProperty;

public sealed class UpdatePropertyHandler(AppDbContext dbContext)
{
    public async Task<Result<PropertyRespose>> HandleAsync(Guid propertyId, UpdatePropertyCommand command, CancellationToken ct)
    {
        var property = await dbContext.Properties
            .FirstOrDefaultAsync(x => x.Id == propertyId, ct);

        if (property is null)
        {
            return Result<PropertyRespose>.Failure(ListingErrors.PropertyNotFound);
        }

        if (property.Status == ListingStatus.Archived)
        {
            return Result<PropertyRespose>.Failure(ListingErrors.ArchivedPropertyCannotBeUpdated);
        }

        var ownerExists = await dbContext.Owners.AnyAsync(x => x.Id == command.OwnerId, ct);

        if (!ownerExists)
        {
            return Result<PropertyRespose>.Failure(ListingErrors.OwnerNotFound);
        }

        // Update property
        property.Update(
            command.OwnerId,
            command.Title,
            command.Description,
            command.Price,
            command.Purpose,
            command.Type,
            command.Bedrooms,
            command.Bathrooms,
            command.Area,
            command.Country,
            command.City,
            command.AddressLine

        );

        await dbContext.SaveChangesAsync(ct);

        return Result<PropertyRespose>.Success(
            new PropertyRespose(
                property.Id,
                property.OwnerId,
                property.Title,
                property.Price,
                property.Purpose,
                property.Type,
                property.Status,
                property.Country,
                property.City
            )
        );

    }
}
