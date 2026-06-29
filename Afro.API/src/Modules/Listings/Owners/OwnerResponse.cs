namespace Afro.API.src.Modules.Listings.Owners;

public sealed record OwnerResponse (
    Guid Id,
    string FullName,
    string PhoneNumber,
    string? Email,
    string? Address
);

