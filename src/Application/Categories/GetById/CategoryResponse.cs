namespace Application.Categories.GetById;

public sealed class CategoryResponse
{
    public Guid Id { get; set; }
    public bool IsActive { get; set; }
    public string Name { get; set; }
    public int MinimumStockQuantity { get; set; }
    public DateTime CreatedDate { get; set; }
}
