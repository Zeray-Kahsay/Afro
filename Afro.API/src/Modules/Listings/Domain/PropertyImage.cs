namespace Afro.API.src.Modules.Listings.Domain;

public sealed class PropertyImage
{
    public Guid Id { get; private set; }
    public Guid PropertyId { get; private set; }
    public string Url { get; private set; } = null!;
    public ImageStorageProvider StorageProvider { get; private set; }
    public string StorageKey { get; private set; } = null!; // The key or identifier used by the storage provider to locate the image
    public bool IsCover { get; private set; }
    public int SortOrder { get; private set; }
    public Property Property { get; private set; } = null!;

    private PropertyImage() { }

    public static PropertyImage Create(
        Guid propertyId,
        string url,
        ImageStorageProvider storageProvider,
        string storageKey,
        int sortOrder
    )
    {
        ArgumentException.ThrowIfNullOrEmpty(url, nameof(url));
        ArgumentException.ThrowIfNullOrEmpty(storageKey, nameof(storageKey));

        if (propertyId == Guid.Empty)
        {
            throw new ArgumentException(
                "Property ID is required",
                nameof(propertyId)
            );
        }

        if (sortOrder < 1)
        {
            throw new ArgumentOutOfRangeException(
                nameof(sortOrder),
                "Sort order must be a positive integer"
            );
        }

        if (!Enum.IsDefined(storageProvider))
        {
            throw new ArgumentOutOfRangeException(
                nameof(storageProvider),
                "Unsupported storage provider"
            );
        }

        var normalizedUrl = url.Trim();
        var normalizedStorageKey = storageKey.Trim();

        if (!Uri.TryCreate(
            normalizedUrl,
            UriKind.Absolute,
            out var uri
        ) || uri.Scheme != Uri.UriSchemeHttps)
        {
            throw new ArgumentException(
                "Image URL must be an absolute HTTPS URL",
                nameof(url)
            );
        }

            return new PropertyImage
            {
                Id = Guid.NewGuid(),
                PropertyId = propertyId,
                Url = normalizedUrl,
                StorageProvider = storageProvider,
                StorageKey = normalizedStorageKey,
                SortOrder = sortOrder
            };
    }

    internal void SetAsCover()
    {
        IsCover = true;
    }

    internal void RemoveCover()
    {
        IsCover = false;
    }


}
