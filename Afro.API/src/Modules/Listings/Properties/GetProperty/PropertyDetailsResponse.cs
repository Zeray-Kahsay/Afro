using Afro.API.src.Modules.Listings.Domain;

namespace Afro.API.src.Modules.Listings.Properties.GetProperty;

public sealed record PropertyDetailsResponse(
    Guid Id,
    Guid OwnerId,
    string OwnerName,
    string OwnerPhoneNumber,
    string Title,
    string Description,
    decimal Price,
    PropertyPurpose Purpose,
    PropertyType Type,
    ListingStatus Status,
    int? Bedrooms,
    int? Bathrooms,
    decimal? Area,
    string Country,
    string City,
    string? AddressLine,
    IReadOnlyCollection<PropertyImageResponse> Images
);

