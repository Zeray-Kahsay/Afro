namespace Afro.API.src.BuildingBlocks.Pagination;

public record CursorPagedResponse<T>(
    IReadOnlyList<T> Items,
    string? NextCursor,
    bool HasMore
    );

