using Afro.API.src.Modules.Listings.Domain;

namespace Afro.API.src.Modules.Listings.Properties.SearchProperties;

public sealed record PropertySummaryResponse(
    Guid Id,
    string Title,
    decimal Price,
    string City,
    string OwnerName,
    string OwnerPhoneNumebr,
    PropertyPurpose Purpose,
    PropertyType Type,
    ListingStatus Status,
    string? CoverImageUrl,
    DateTime CreatedAtUtc
);



