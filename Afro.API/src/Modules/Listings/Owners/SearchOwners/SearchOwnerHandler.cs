using Afro.API.src.BuildingBlocks.Pagination;
using Afro.API.src.BuildingBlocks.Results;
using Afro.API.src.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Afro.API.src.Modules.Listings.Owners.SearchOwners;

public sealed class SearchOwnerHandler(AppDbContext dbContext)
{
    public async Task<Result<CursorPagedResponse<OwnerResponse>>> HandleSearchOwnerAsync(
        SearchOwnerRequest query,
        CancellationToken ct
    )
    {
        var owners = dbContext.Owners
                .AsNoTracking();

        owners = query.Status switch
        {
            OwnerStatusFilter.Active => owners.Where(o => !o.IsArchived),

            OwnerStatusFilter.Archived => owners.Where(o => o.IsArchived),

            OwnerStatusFilter.All => owners,

            _ => owners
        };

        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            owners = owners.Where(x => x.FullName.Contains(query.Search) || x.PhoneNumber.Contains(query.Search));
        }

        // Cursor
        var cursor = CursorEncoder.Decode(query.Cursor);

        if (cursor is not null)
        {
            owners = owners.Where(o => o.CreatedAtUtc < cursor.CreatedAtUtc ||
                (o.CreatedAtUtc == cursor.CreatedAtUtc && o.Id.CompareTo(cursor.Id) < 0));
        }

        // Ensure ordering matches cursor predicate: CreatedAtUtc DESC, Id DESC
        owners = owners
            .OrderByDescending(o => o.CreatedAtUtc)
            .ThenByDescending(o => o.Id);

        // Fetch one extra and include CreatedAtUtc/Id for cursor calculation
        var fetched = await owners
                .Take(query.PageSize + 1)
                .Select(x => new
                {
                    x.Id,
                    x.CreatedAtUtc,
                    x.FullName,
                    x.PhoneNumber,
                    x.Email,
                    x.Address,
                    Notes = x.Notes ?? "No notes available."
                }).ToListAsync(ct);

        var hasMore = fetched.Count > query.PageSize;

        var pageItems = hasMore ? fetched.Take(query.PageSize).ToList() : fetched;

        var nextCursor = hasMore && pageItems.Any()
            ? CursorEncoder.Encode(new CursorToken(pageItems.Last().CreatedAtUtc, pageItems.Last().Id))
            : null;

        var items = pageItems.Select(x => new OwnerResponse(
            x.Id,
            x.FullName,
            x.PhoneNumber,
            x.Email,
            x.Address,
            x.Notes
        )).ToList();

        return Result<CursorPagedResponse<OwnerResponse>>.Success(
            new CursorPagedResponse<OwnerResponse>(
                items,
                nextCursor,
                hasMore
            )
        );
    }
}
