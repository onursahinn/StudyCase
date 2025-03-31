using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Product;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Products.GetById;

public sealed class GetProductByIdQueryHandler(IApplicationDbContext context)
    : IQueryHandler<GetProductByIdQuery, ProductResponse>
{
    public async Task<Result<ProductResponse>> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
     {
         ProductResponse? data = await context.Products.Include(x=>x.Category).AsNoTracking()
             .Where(p => p.Id == query.ProductId )
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
             .SingleOrDefaultAsync(cancellationToken);

         if (data is null)
         {
             return Result.Failure<ProductResponse>(ProductErrors.NotFound(query.ProductId));
         }

         return data;
     }
}
