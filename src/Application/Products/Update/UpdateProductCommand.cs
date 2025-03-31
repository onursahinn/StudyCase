using Application.Abstractions.Messaging;
using Application.Products.GetById;

namespace Application.Products.Update;

public class UpdateProductCommand : ICommand<ProductResponse>
{
    public Guid ProductId { get; set; }
    public bool IsActive { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int StockQuantity { get; set; }
    public Guid CategoryId { get; set; }
}
