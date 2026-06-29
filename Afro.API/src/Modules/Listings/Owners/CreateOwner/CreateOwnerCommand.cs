namespace Afro.API.src.Modules.Listings.Owners.CreateOwner;

public sealed record CreateOwnerCommand(
    string FullName,
    string PhoneNumber,
    string? Email,
    string? Address,
    string? Notes

);

