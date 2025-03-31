using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Products.GetById;
using Domain.Product;
using Microsoft.EntityFrameworkCore;
using SharedKernel;


namespace Application.Products.Get;

public sealed class GetProductsQueryHandler(IApplicationDbContext context) : IQueryHandler<GetProductsQuery,PaginatedResult<ProductResponse>>
{
    public async Task<Result<PaginatedResult<ProductResponse>>> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        IQueryable<Product> dbQuery = context.Products.Include(x=>x.Category).AsNoTracking().AsQueryable();
        
        if (!string.IsNullOrEmpty(query.Search))
        {
            dbQuery = dbQuery.Where(p => EF.Functions
                .ToTsVector("turkish", p.Title + " " + p.Description + " " + p.Category.Name)
                .Matches(EF.Functions.ToTsQuery("turkish", query.Search)))
                .OrderByDescending(p => EF.Functions.ToTsVector("turkish", p.Title + " " + p.Description + " " + p.Category.Name).Rank(
                    EF.Functions.ToTsQuery("turkish", query.Search)
                ));
        }
        if (query.MinimumStockQuantity.HasValue)
        {
            dbQuery = dbQuery.Where(x => x.StockQuantity >= query.MinimumStockQuantity);
        }

        if (query.MaximumStockQuantity.HasValue)
        {
            dbQuery = dbQuery.Where(x => x.StockQuantity <= query.MaximumStockQuantity);
        }
        
        int total = await dbQuery.CountAsync(cancellationToken);

        List<ProductResponse> data = await dbQuery
            .Skip(query.Offset)
            .Take(query.Limit > Constants.MaxLimit ? Constants.MaxLimit : query.Limit)
            .Select(x => new ProductResponse()
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                IsActive = x.IsActive,
                Category =  new CategoryLookUp()
                {
                    Id = x.Category.Id,
                    Name = x.Category.Name,
                },
                StockQuantity = x.StockQuantity,
                CreatedDate= x.CreatedDate,
            })
            .ToListAsync(cancellationToken);

        return new PaginatedResult<ProductResponse>(query.Limit, query.Offset, total, data);
    }
    
}
