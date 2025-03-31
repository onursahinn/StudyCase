using Application.Abstractions.Messaging;
using Application.Categories.GetById;
using SharedKernel;

namespace Application.Categories.Get;

public sealed record GetCategoriesQuery(
    string? Search,
    int Limit,
    int Offset) : IQuery<PaginatedResult<CategoryResponse>>
{
}
