namespace Afro.API.src.BuildingBlocks.Pagination;

public sealed record CursorToken(DateTime CreatedAtUtc, Guid Id);

