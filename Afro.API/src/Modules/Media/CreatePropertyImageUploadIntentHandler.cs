using Afro.API.Modules.BuildingBlocks.Results;
using Afro.API.src.BuildingBlocks.Results;
using Afro.API.src.BuildingBlocks.Storage;
using Afro.API.src.Infrastructure.Persistence;
using Afro.API.src.Modules.Listings.Constants;
using Afro.API.src.Modules.Listings.Domain;
using Microsoft.EntityFrameworkCore;

namespace Afro.API.src.Modules.Media;

public sealed class CreatePropertyImageUploadIntentHandler(AppDbContext context, IMediaStorageProvider mediaStorageProvider)
{
    private static readonly TimeSpan UploadIntentLifeTime = TimeSpan.FromMinutes(10);

    public async Task<Result<CreatePropertyImageUploadIntentResponse>> HandleAsync(Guid propertyId, CancellationToken ct)
    {
        var property = await context.Properties
                .FirstOrDefaultAsync(x => x.Id == propertyId, ct);

        if (property is null)
        {
            return Result<CreatePropertyImageUploadIntentResponse>.Failure(ListingErrors.PropertyNotFound);
        } 

        if (property.Status == Listings.Domain.ListingStatus.Archived)
        {
            return Result<CreatePropertyImageUploadIntentResponse>.Failure(ListingErrors.ArchivedPropertyCannotBeUpdated);
        }

        var nowUtc = DateTime.UtcNow;
        var expiresAtUtc = nowUtc.Add(UploadIntentLifeTime);

        //Backend controls the storage key
        var storageKey = $"properties/{propertyId:N}/{Guid.NewGuid() :N}";

        var storageProvider = ImageStorageProvider.Cloudinary;
        var uploadIntent = PropertyImageUploadIntent.Create(
            propertyId,
            storageProvider,
            storageKey,
            nowUtc,
            expiresAtUtc
        );


        var signature = mediaStorageProvider.GenerateUploadSignature(storageKey);
        context.PropertyImageUploadIntents.Add(uploadIntent);

        await context.SaveChangesAsync(ct);

        return Result<CreatePropertyImageUploadIntentResponse>.Success(
            new CreatePropertyImageUploadIntentResponse(
                    uploadIntent.Id,
                    signature.CloudName,
                    signature.ApiKey,
                    signature.Timestamp,
                    signature.Signature,
                    signature.StorageKey,
                    uploadIntent.ExpiresAtUtc
            )
        );
    }
}
