using Afro.API.src.Modules.Listings.Domain;

namespace Afro.API.src.Modules.Listings.Properties;

public sealed record PropertyRespose(
    Guid Id,
    Guid OwnerId,
    string Title,
    decimal Price,
    PropertyPurpose Purpose,
    PropertyType Type,
    ListingStatus Status,
    string Country,
    string City
);



