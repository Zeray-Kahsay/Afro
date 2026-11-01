namespace Afro.API.src.Modules.Listings.Domain;

public sealed class PropertyImageUploadIntent
{
    public Guid Id  { get; private set; }
    public Guid  PropertyId  { get; private set; }
    public ImageStorageProvider StorageProvider  { get; private set; }

    // Cloudinary public Id during dev; Azure blob name in Prod
    public string  StorageKey  { get; private set; } = null!;
    public DateTime  CreatedAtUtc  { get; private set; }
    public DateTime  ExpiresAtUtc  { get; private set; }
    public DateTime?  FinalizedAtUtc  { get; private set; }
    public bool IsFinalized => FinalizedAtUtc.HasValue;
    public Property Property  { get; private set; } = null!;
    private PropertyImageUploadIntent() {}

    public static PropertyImageUploadIntent Create (
        Guid propertyId,
        ImageStorageProvider storageProvider,
        string storageKey,
        DateTime createdAtUtc,
        DateTime expiresAtUtc

    )
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(storageKey);

        if (propertyId == Guid.Empty)
        {
            throw new ArgumentException("Property ID is required", nameof(propertyId));
        }

        if (!Enum.IsDefined(storageProvider))
        {
            throw new ArgumentException("Invalid storage provider", nameof(storageProvider));
        }

        if (expiresAtUtc <= createdAtUtc)
        {
            throw new ArgumentException("Expiration must be later than creation", nameof(expiresAtUtc));
        }

        return new PropertyImageUploadIntent
        {
            Id = Guid.NewGuid(),
            PropertyId = propertyId,
            StorageProvider = storageProvider,
            StorageKey = storageKey,
            CreatedAtUtc = createdAtUtc,
            ExpiresAtUtc = expiresAtUtc
        };
    }

    public void MarkFinalized(DateTime nowUtc)
    {
        if (IsFinalized)
        {
            throw new InvalidOperationException("Upload intent has already been finalized.");
        }

        if (nowUtc >= ExpiresAtUtc)
        {
            throw new InvalidOperationException("Upload intent has expired.");
        }

        FinalizedAtUtc = nowUtc;
    }

}
