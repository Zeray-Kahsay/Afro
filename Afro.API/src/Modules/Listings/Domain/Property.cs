namespace Afro.API.src.Modules.Listings.Domain;

public sealed class Property
{
    private const int MaxImages = 30;
    public Guid Id { get; private set; }
    public string Title { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public decimal Price { get; private set; }
    public PropertyPurpose Purpose { get; private set; }
    public PropertyType Type { get; private set; }
    public int? Bedrooms { get; private set; }
    public int? Bathrooms { get; private set; }
    public decimal? Area { get; private set; }
    public string Country { get; private set; } = null!;
    public string City { get; private set; } = null!;
    public string? AddressLine { get; private set; }
    public ListingStatus Status { get; private set; }
    public Guid OwnerId { get; private set; }
    public Owner Owner { get; private set; } = null!;
    public DateTime CreatedAtUtc { get; private set; }
    public DateTime UpdatedAtUtc { get; private set; }
    public DateTime? ArchivedAtUtc { get; private set; }

    private readonly List<PropertyImage> _images = [];
    public IReadOnlyCollection<PropertyImage> Images => _images.AsReadOnly();


    private Property() { }

    public static Property Create(
        Guid ownerId,
        string title,
        string description,
        decimal price,
        PropertyPurpose purpose,
        PropertyType type,
        int? bedrooms,
        int? bathrooms,
        decimal? area,
        string country,
        string city,
        string? addressLine

    )
    {
        return new Property
        {
            Id = Guid.NewGuid(),
            OwnerId = ownerId,
            Title = title,
            Description = description,
            Price = price,
            Purpose = purpose,
            Type = type,
            Bedrooms = bedrooms,
            Bathrooms = bathrooms,
            Area = area,
            Country = country,
            City = city,
            AddressLine = addressLine,
            Status = ListingStatus.Draft,
            CreatedAtUtc = DateTime.UtcNow,
            UpdatedAtUtc = DateTime.UtcNow,
        };
    }

    public void AddImage(string url)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(url, nameof(url));

        url = url.Trim();

        if (_images.Any(x => x.Url.Equals(url, StringComparison.OrdinalIgnoreCase)))
        {
            throw new InvalidOperationException("Image with this URL already exists");
        }

        if (_images.Count >= MaxImages)
        {
            throw new InvalidOperationException("Maximum number of images exceeded");
        }

        _images.Add(PropertyImage.Create(Id, url, _images.Count + 1));
        UpdatedAtUtc = DateTime.UtcNow;
    }

    public bool HasCoverImage() => _images.Any(x => x.IsCover);

    public void SetCoverImage(Guid imageId)
    {
        var image = _images.FirstOrDefault(x => x.Id == imageId)
            ?? throw new InvalidOperationException("Image does not belong to property");

        foreach (var propertyImage in _images)
        {
            propertyImage.RemoveCover();
        }

        image.SetAsCover();
        UpdatedAtUtc = DateTime.UtcNow;
    }

    public void Publish()
    {
        if (_images.Count == 0)
        {
            throw new InvalidOperationException("Property must contain at least one image");
        }

        if (!HasCoverImage())
        {
            throw new InvalidOperationException("Property must contain a cover image.");
        }

        Status = ListingStatus.Published;
        UpdatedAtUtc = DateTime.UtcNow;
    }


    public void Archive()
    {
        if (Status == ListingStatus.Archived)
        {
            throw new InvalidOperationException("Property is already archived");
        }

        Status = ListingStatus.Archived;

        ArchivedAtUtc = DateTime.UtcNow;
    }

    public void Update(
        Guid ownerId,
        string title,
        string description,
        decimal price,
        PropertyPurpose purpose,
        PropertyType type,
        int? bedrooms,
        int? bathrooms,
        decimal? area,
        string country,
        string city,
        string? addressLine
    )
    {
        OwnerId = ownerId;
        Title = title;
        Description = description;
        Price = price;
        Purpose = purpose;
        Type = type;
        Bedrooms = bedrooms;
        Bathrooms = bathrooms;
        Area = area;
        Country = country;
        City = city;
        AddressLine = addressLine;
        UpdatedAtUtc = DateTime.UtcNow;
    }
}
