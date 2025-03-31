using Application.Abstractions.Messaging;
using Application.Categories.GetById;

namespace Application.Categories.Update;

public sealed class UpdateCategoryCommand : ICommand<CategoryResponse>
{
    public Guid CategoryId { get; set; }
    public bool IsActive { get; set; }
    public string Name { get; set; }
    public int MinimumStockQuantity { get; set; }
}
