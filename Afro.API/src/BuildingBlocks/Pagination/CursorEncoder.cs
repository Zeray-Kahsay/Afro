using System.Text;

namespace Afro.API.src.BuildingBlocks.Pagination;

public static class CursorEncoder
{
    public static string Encode (CursorToken token)
    {
        var value = $"{token.CreatedAtUtc:0} | {token.Id}";

        return Convert.ToBase64String(Encoding.UTF8.GetBytes(value));
    }

    public static CursorToken? Decode(string? cursor)
    {
        if (string.IsNullOrWhiteSpace(cursor))
            return null;
        
        try
        {
            var decoded = Encoding.UTF8.GetString(Convert.FromBase64String(cursor));
            var parts = decoded.Split('|');

            return new CursorToken(DateTime.Parse(parts[0]), Guid.Parse(parts[1]));
        }
        catch 
        {
            return null;
        }
        
    }
}
