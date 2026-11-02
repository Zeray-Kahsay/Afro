using Afro.API.src.BuildingBlocks.Results;
using Afro.API.src.BuildingBlocks.Storage;
using Afro.API.src.Infrastructure.Persistence;
using Afro.API.src.Modules.Listings.Constants;
using Afro.API.src.Modules.Listings.Domain;
using Microsoft.EntityFrameworkCore;

namespace Afro.API.src.Modules.Listings.Properties.UploadPropertyImages;

public class UploadPropertyImagesHandler(AppDbContext dbContext, IMediaStorageProvider mediaStorageProvider)
{
    private const int MaxImageSizeInBytes = 5 * 1024 * 1024; // 5 MB
    private const int MaxImagesPerRequest = 30;
    private static readonly HashSet<string> AllowedFormats = [
        "jpg",
        "png",
        "webp",
        "jpeg"
    ];

    public async Task<Result<UploadPropertyImagesResponse>> HandleAsync(
        Guid propertyId,
        UploadPropertyImagesCommand command,
        CancellationToken ct)
    {

        //1. Validate request
        if (propertyId == Guid.Empty
            || command.Images is null
            || command.Images.Count == 0
            || command.Images.Count > MaxImagesPerRequest)
        {
            return Result<UploadPropertyImagesResponse>.Failure(ListingErrors.InvalidImageUploadRequest);
        }

        var uploadIntentIds = command.Images.Select(x => x.UploadIntentId).ToList();

        if (uploadIntentIds.Any(x => x == Guid.Empty) || 
            uploadIntentIds.Distinct().Count() != uploadIntentIds.Count)
        {
            return Result<UploadPropertyImagesResponse>.Failure(ListingErrors.InvalidImageUploadRequest);
        }

        //2. Retrieve property and existing images
        var property = await dbContext.Properties
                            .Include(x => x.Images)
                            .FirstOrDefaultAsync(x => x.Id == propertyId,
                                ct);

        if (property is null)
        {
            return Result<UploadPropertyImagesResponse>.Failure(ListingErrors.PropertyNotFound);
        }

        if (property.Status == Domain.ListingStatus.Archived)
        {
            return Result<UploadPropertyImagesResponse>.Failure(ListingErrors.ArchivedPropertyCannotBeUpdated);
        }

        //3. Enforce property image limit
        if (property.Images.Count + uploadIntentIds.Count > Property.MaximumImages)
        {
            return Result<UploadPropertyImagesResponse>.Failure(ListingErrors.PropertyImageLimitExceeded);
        }

        //4. Retrieve upload intents
        var uploadIntents = await dbContext.PropertyImageUploadIntents
            .Where(x => uploadIntentIds.Contains(x.Id)
                && x.PropertyId == propertyId) 
            .ToListAsync(ct);
        
        if (uploadIntents.Count != uploadIntentIds.Count)
        {
            return Result<UploadPropertyImagesResponse>.Failure(ListingErrors.UploadIntentNotFound);
        }

        //5. Validate uplad intents
        var nowUtc = DateTime.UtcNow;

        foreach (var intent in uploadIntents)
        {
            if (intent.PropertyId != propertyId)
            {
                return Result<UploadPropertyImagesResponse>.Failure(ListingErrors.UploadIntentDoesNotBelongToProperty);
            }

            if (intent.IsFinalized)
            {
                return Result<UploadPropertyImagesResponse>.Failure(ListingErrors.UploadIntentAlreadyFinalized);
            }

            if (nowUtc >= intent.ExpiresAtUtc)
            {
                return Result<UploadPropertyImagesResponse>.Failure(ListingErrors.UploadIntentExpired);
            }

            if (intent.StorageProvider != Domain.ImageStorageProvider.Cloudinary)
            {
                return Result<UploadPropertyImagesResponse>.Failure(ListingErrors.InvalidImageUploadRequest);
            }
        }

        //6.Verify every uploaded asset against Cloudinary
        var verifiedAssets  = new List<VerfiedMediaAsset>();

        foreach (var intent in uploadIntents)
        {
            var asset = await mediaStorageProvider.VerfiyImageAsync(intent.StorageKey, ct);

            if (asset is null)
            {
                return Result<UploadPropertyImagesResponse>.Failure(ListingErrors.UploadAssetNotFound);
            }

            // Ensure the returned asset matches the intent
            if (!string.Equals(
                asset.StorageKey,
                intent.StorageKey,
                StringComparison.Ordinal
            ))
            {
                return Result<UploadPropertyImagesResponse>.Failure(ListingErrors.InvalidUploadedImage);
            }

            // Validate actual file size
            if (asset.Bytes <= 0 || asset.Bytes > MaxImageSizeInBytes)
            {
                return Result<UploadPropertyImagesResponse>.Failure(ListingErrors.InvalidUploadedImage);
            }

            // Validate actual file format
            if (string.IsNullOrWhiteSpace(asset.Format) || !AllowedFormats.Contains(asset.Format.ToLowerInvariant()))
            {
                return Result<UploadPropertyImagesResponse>.Failure(ListingErrors.InvalidUploadedImage);
            }

            // Validate secure URL
            if (!Uri.TryCreate(asset.SecureUrl, UriKind.Absolute, out var secureUri) || 
                secureUri.Scheme != Uri.UriSchemeHttps)
            {
                return Result<UploadPropertyImagesResponse>.Failure(ListingErrors.InvalidUploadedImage);
            }

            verifiedAssets.Add(asset);
        }

        //7. Start database transaction
        await using var transaction = await dbContext.Database.BeginTransactionAsync(ct);

        try
        {
            //8. Associate verified assets with the property
            foreach (var (intent, asset) in uploadIntents.Zip(verifiedAssets))
            {
                property.AddImage(
                    asset.SecureUrl,
                    intent.StorageProvider,
                    asset.StorageKey);
                
                intent.MarkFinalized(nowUtc);
            }

            //9. Persist all changes atomically
            await dbContext.SaveChangesAsync(ct);

            await transaction.CommitAsync(ct);

            return Result<UploadPropertyImagesResponse>.Success(
                new UploadPropertyImagesResponse(property.Id, property.Images.Count));
        }
        catch (DbUpdateConcurrencyException)
        {
            await transaction.RollbackAsync(ct);
            
            return Result<UploadPropertyImagesResponse>.Failure(ListingErrors.UploadIntentAlreadyFinalized);
        }
        catch (DbUpdateException)
        {
            await transaction.RollbackAsync(ct);

            throw; // Rethrow the exception to be handled by higher-level error handling middleware or logging
            
        }

    }
}
