namespace Afro.API.src.Modules.Listings.Properties.SearchProperties;

public sealed record SearchPropertiesResponse(
    IReadOnlyCollection<PropertySummaryResponse> Properties,
    DateTime? NextCursorCreatedAtUtc,
    Guid? NextCursorId
);



