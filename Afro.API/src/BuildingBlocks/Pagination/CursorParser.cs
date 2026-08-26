namespace Afro.API.src.BuildingBlocks.Pagination;

public static class CursorParser
{
    public static Guid? Parse(string? cursor)
    {
        if (string.IsNullOrWhiteSpace(cursor))
                return null;
        
        return Guid.TryParse(cursor, out var value)
                 ? value 
                 : null;
    }
}
