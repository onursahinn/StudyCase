using NpgsqlTypes;
using SharedKernel;

namespace Domain.Category;

public class Category : Entity
{
    public string Name { get; set; }
    public int MinimumStockQuantity { get; set; }
    public virtual List<Product.Product> Products { get; set; }
    public NpgsqlTsVector SearchVector { get; set; }  // Add this property

}
