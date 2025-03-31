using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Category;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Categories.GetById;

public sealed class GetCategoryByIdQueryHandler(IApplicationDbContext context) : IQueryHandler<GetCategoryByIdQuery, CategoryResponse>
{
    public async Task<Result<CategoryResponse>> Handle(GetCategoryByIdQuery query, CancellationToken cancellationToken)
    {
        CategoryResponse? data = await context.Categories.AsNoTracking()
            .Where(c => c.Id == query.CategoryId )
            .Select(x => new CategoryResponse()
            {
                Id = x.Id,
                Name = x.Name,
                MinimumStockQuantity = x.MinimumStockQuantity,
                IsActive = x.IsActive,
                CreatedDate= x.CreatedDate,
            })
            .SingleOrDefaultAsync(cancellationToken);

        if (data is null)
        {
            return Result.Failure<CategoryResponse>(CategoryErrors.NotFound(query.CategoryId));
        }

        return data;
    }
}
