using Application.Abstractions.Messaging;
using Application.Products.GetById;
using SharedKernel;

namespace Application.Products.Get;

public sealed record GetProductsQuery(
    string? Search,
    int? MinimumStockQuantity,
    int? MaximumStockQuantity,
    int Limit,
    int Offset) : IQuery<PaginatedResult<ProductResponse>>
{
}
