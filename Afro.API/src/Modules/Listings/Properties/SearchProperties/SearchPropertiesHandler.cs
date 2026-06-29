using Afro.API.src.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Afro.API.src.Modules.Listings.Properties.SearchProperties;

public sealed class SearchPropertiesHandler(AppDbContext dbContext)
{
    public async Task<SearchPropertiesResponse> HandleAsync(SearchPropertiesQuery query, CancellationToken ct)
    {
        var properties = dbContext.Properties
                    .AsNoTracking()
                    .Include(x => x.Owner)
                    .Include(x => x.Images)
                    .AsQueryable();
        
        // Search by TITLE || DESCRIPTION
        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            properties = properties.Where(x => 
                x.Title.Contains(query.Search) || 
                x.Description.Contains(query.Search)
            );
        }

        // Search by OWNER
        if (!string.IsNullOrWhiteSpace(query.OwnerPhoneNumber))
        {
            properties = properties.Where(x => x.Owner.PhoneNumber == query.OwnerPhoneNumber);
        }

        //Search by PURPOSE
        if (query.Purpose is not null)
        {
            properties = properties.Where(x => x.Purpose == query.Purpose);
        }

        // Search by TYPE
        if (query.Type is not null)
        {
            properties = properties.Where(x => x.Type == query.Type);
        }

        // Search by STATUS
        if (query.Status is not null)
        {
            properties = properties.Where(x => x.Status == query.Status);
        }

        // Country
        if (!string.IsNullOrWhiteSpace(query.Country))
        {
            properties = properties.Where(x => x.Country == query.Country);
        }

        // City
        if (!string.IsNullOrWhiteSpace(query.City))
        {
            properties = properties.Where(x => x.City == query.City);
        }

        // Price

        if (query.MinPrice is not null)
        {
            properties = properties.Where(x => x.Price >= query.MinPrice);
        }

        if (query.MaxPrice is not null)
        {
            properties = properties.Where(x => x.Price <= query.MaxPrice);
        }

        // KEYSET PAGINATION
        if (query.CursorCreateAtUtc is not null && query.CursorId is not null)
        {
            properties = properties.Where(x => x.CreatedAtUtc < query.CursorCreateAtUtc ||
            (
                x.CreatedAtUtc == query.CursorCreateAtUtc && x.Id.CompareTo(query.CursorId.Value) < 0
            ));
        }

        // FETCH
        var results = await properties
                .OrderByDescending(x => x.CreatedAtUtc)
                .ThenByDescending(x => x.Id)
                .Take(query.PageSize + 1)
                .Select(x => new PropertySummaryResponse(
                    x.Id,
                    x.Title,
                    x.Price,
                    x.City,
                    x.Owner.FullName,
                    x.Owner.PhoneNumber,
                    x.Purpose,
                    x.Type,
                    x.Status,
                    x.Images
                        .Where(i => i.IsCover)
                        .Select(i => i.Url)
                        .FirstOrDefault(),
                    x.CreatedAtUtc
                )).ToListAsync(ct);


        // CURSOR CALCULATION
        var hasNext = results.Count > query.PageSize;

        if (hasNext)
        {
            results.RemoveAt(results.Count - 1);
        }

        var last = results.LastOrDefault();

        return new SearchPropertiesResponse(
            results,
            hasNext
            ? last?.CreatedAtUtc
            : null,
            hasNext ? last?.Id : null
        );

    }

}
