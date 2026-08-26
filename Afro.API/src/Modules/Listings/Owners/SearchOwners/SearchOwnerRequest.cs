namespace Afro.API.src.Modules.Listings.Owners.SearchOwners;

public sealed record SearchOwnerRequest(
    string? Search,
    OwnerStatusFilter Status = OwnerStatusFilter.Active,
    string? Cursor = null,
    int PageSize = 20
);

    

