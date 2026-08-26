using Afro.API.src.Infrastructure.Persistence;
using Afro.API.src.Modules.Listings.Domain;
using Microsoft.EntityFrameworkCore;

namespace Afro.API.src.Modules.Listings.common;

public static class OwnerQueries
{
    public static Task<Owner?> FindOwnerAsync(
        this AppDbContext context,
        Guid ownerId,
        CancellationToken ct
    )
    {
        return context.Owners.FirstOrDefaultAsync(o => 
              o.Id == ownerId && !o.IsArchived, ct);
    }

    public static Task<Owner?> FindOwnerIncludingArchivedAsync(
        this AppDbContext context,
        Guid ownerId,
        CancellationToken ct
    )
    {
        return context.Owners
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(x => x.Id == ownerId, ct);
    }
}
