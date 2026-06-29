using Afro.API.src.Modules.Listings.Domain;

namespace Afro.API.src.Modules.Listings.Properties.SearchProperties;

public sealed class SearchPropertiesQuery
{
    public string? Search { get; init; }
    public string?  OwnerPhoneNumber  { get; init; }
    public PropertyPurpose? Purpose  { get; init; }
    public PropertyType? Type  { get; init; }
    public ListingStatus? Status  { get; init; }
    public string? Country { get; init; }
    public string? City { get; init; }
    public decimal? MinPrice  { get; init; }
    public decimal? MaxPrice { get; init; }
    public DateTime? CursorCreateAtUtc { get; init; }
    public Guid? CursorId { get; init; }
    public int PageSize  { get; init; } = 20;

}


