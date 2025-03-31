using Domain.Category;

namespace Application.Products.GetById;

public sealed class ProductResponse
{
    public Guid Id { get; set; }
    public bool IsActive { get; set; }
    public CategoryLookUp Category { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int StockQuantity { get; set; }
    public DateTime CreatedDate { get; set; }
}

public sealed class CategoryLookUp
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
