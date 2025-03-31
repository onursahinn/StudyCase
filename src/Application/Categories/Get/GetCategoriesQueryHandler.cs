using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Categories.GetById;
using Application.Products.GetById;
using Domain.Category;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Categories.Get;

public sealed class GetCategoriesQueryHandler(IApplicationDbContext context) : IQueryHandler<GetCategoriesQuery,PaginatedResult<CategoryResponse>>
{
    public async Task<Result<PaginatedResult<CategoryResponse>>> Handle(GetCategoriesQuery query, CancellationToken cancellationToken)
    {
        IQueryable<Category> dbQuery = context.Categories.AsNoTracking().AsQueryable();
        
        if (!string.IsNullOrEmpty(query.Search))
        {
            dbQuery = dbQuery.Where(p => EF.Functions
                    .ToTsVector("turkish", p.Name )
                    .Matches(EF.Functions.ToTsQuery("turkish", query.Search)))
                .OrderByDescending(p => EF.Functions.ToTsVector("turkish", p.Name).Rank(
                    EF.Functions.ToTsQuery("turkish", query.Search)
                ));
        }
        
        int total = await dbQuery.CountAsync(cancellationToken);

        List<CategoryResponse> data = await dbQuery
            .Skip(query.Offset)
            .Take(query.Limit > Constants.MaxLimit ? Constants.MaxLimit : query.Limit)
            .Select(x => new CategoryResponse()
            {
                Id = x.Id,
                Name = x.Name,
                MinimumStockQuantity = x.MinimumStockQuantity,
                IsActive = x.IsActive,
                CreatedDate= x.CreatedDate,
            })
            .ToListAsync(cancellationToken);

        return new PaginatedResult<CategoryResponse>(query.Limit, query.Offset, total, data);
    }
}
