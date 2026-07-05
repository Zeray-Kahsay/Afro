namespace Afro.API.src.Modules.Listings.Owners.UpdateOwner;

public sealed record UpdateOwnerCommand(
    Guid OwnerId,
    string FullName,
    string PhoneNumber,
    string? Email,
    string? Address,
    string? Notes
);



