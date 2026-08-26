namespace Afro.API.src.Modules.Listings.Owners.SearchOwners;

public sealed record SearchOwnerQuery(
    string? Search,
    OwnerStatusFilter Status,
    string? Cursor,
    int PageSize
    );



