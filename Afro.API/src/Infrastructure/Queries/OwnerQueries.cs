using Afro.API.src.Infrastructure.Persistence;
using Afro.API.src.Modules.Listings.Domain;
using Microsoft.EntityFrameworkCore;

namespace Afro.API.src.Infrastructure.Queries;

public static class OwnerQueries
{
    public static IQueryable<Owner> Active(this IQueryable<Owner> query)
    {
        return query.Where(o => !o.IsArchived);
    }

    public static IQueryable<Owner> Archived (this IQueryable<Owner> query)
    {
        return query.Where(o => o.IsArchived);
    }

    public static Task<Owner?> GetActiveByIdAsync(this AppDbContext context, Guid ownerId, CancellationToken ct)
    {
        return context.Owners
            .Where(o => !o.IsArchived)
            .FirstOrDefaultAsync(o => o.Id == ownerId, ct);
    }

    public static Task<Owner?> GetByIdIncludingArchivedAsync(this AppDbContext context, Guid ownerId, CancellationToken ct)
    {
        return context.Owners.FirstOrDefaultAsync(o => o.Id == ownerId, ct);
    }

    
}
