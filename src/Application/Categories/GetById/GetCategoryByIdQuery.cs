using Application.Abstractions.Messaging;

namespace Application.Categories.GetById;

public sealed record class GetCategoryByIdQuery(Guid CategoryId) : IQuery<CategoryResponse>
{
    
}
