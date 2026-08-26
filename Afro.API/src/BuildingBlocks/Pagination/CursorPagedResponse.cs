namespace Afro.API.src.BuildingBlocks.Pagination;

public class CursorPagedResponse<T> (
    IReadOnlyList<T> Items,
    string? NextCursor,
    bool HasMore
    );

