using Application.Abstractions.Messaging;

namespace Application.Products.Create;

public sealed class CreateProductCommand : ICommand<Guid>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int StockQuantity { get; set; }
    public Guid CategoryId { get; set; }
}
