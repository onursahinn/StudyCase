using NpgsqlTypes;
using SharedKernel;

namespace Domain.Product;

public class Product : Entity
{ 
    public Guid CategoryId { get; set; }
    public Category.Category Category { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int StockQuantity { get; set; }
    public string SearchText { get; set; } 
    public NpgsqlTsVector SearchVector { get; set; }

}
