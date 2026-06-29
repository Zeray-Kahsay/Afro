using Afro.API.src.Modules.Listings.Domain;

namespace Afro.API.src.Modules.Listings.Properties.CreateProperty;

public sealed record CreatePropertyCommand(
    Guid OwnerId,
    string Title,
    string Description,
    decimal Price,
    PropertyPurpose Purpose,
    PropertyType Type,
    int? Bedrooms,
    int? Bathrooms,
    decimal? Area,
    string Country,
    string City,
    string? AddressLine
);



