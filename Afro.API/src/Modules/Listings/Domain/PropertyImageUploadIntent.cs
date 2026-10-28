namespace Afro.API.src.Modules.Listings.Domain;

public sealed class PropertyImageUploadIntent
{
    public Guid Id  { get; set; }
    public Guid  PropertyId  { get; set; }
    public ImageStorageProvider StorageProvider  { get; set; }

    // Cloudinary public Id during dev; Azure blob name in Prod
    public string  StorageKey  { get; set; } = null!;
    public DateTime  CreatedAtUtc  { get; set; }
    public DateTime  ExpiresAtUtc  { get; set; }
    public DateTime?  FinalizedAtUtc  { get; set; }
    public bool IsFinalized => FinalizedAtUtc.HasValue;
    public Property Property  { get; set; } = null!;
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
