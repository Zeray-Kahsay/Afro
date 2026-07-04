using Afro.API.src.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Afro.API.src.Modules.Listings.Owners.SearchOwners;

public sealed class SearchOwnerHandler(AppDbContext dbContext)
{
    public async Task<List<OwnerResponse>> HandleSearchOwnerAsync(
        SearchOwnerQuery query,
        CancellationToken ct
    )
    {
        var owners = dbContext.Owners.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            owners = owners.Where(x => x.FullName.Contains(
                query.Search) || x.PhoneNumber.Contains(query.Search)
            );
        }

        return await owners
            .OrderBy(x => x.FullName)
            .Take(20)
            .Select(x => new OwnerResponse(
                x.Id,
                x.FullName,
                x.PhoneNumber,
                x.Email,
                x.Address,
                x.Notes ?? "No notes available."
            )).ToListAsync(ct);
    }
}
