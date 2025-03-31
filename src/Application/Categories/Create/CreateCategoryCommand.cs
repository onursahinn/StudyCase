using Application.Abstractions.Messaging;

namespace Application.Categories.Create;

public sealed class CreateCategoryCommand : ICommand<Guid>
{
    public string Name { get; set; }
    public int MinimumStockQuantity { get; set; }
}
