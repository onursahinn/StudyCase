using SharedKernel;

namespace Domain.Product;

public static class ProductErrors
{
    public static Error NotFound(Guid productId) => Error.NotFound(
        "Products.NotFound",
        $"The product with the Id = '{productId}' was not found");
    
    public static Error TitleLength() => Error.Failure(
        "Products.TitleLength",
        "Product title must be between 1 and 200 characters.");
    
    public static Error TitleNotEmpty() => Error.Failure(
        "Products.TitleNotEmpty",
        "Product title must not be empty.");
    
    public static Error CategoryNotEmpty() => Error.Failure(
        "Products.CategoryNotEmpty",
        "Product category must not be empty.");
    
    public static Error IdNotEmpty() => Error.Failure(
        "Products.IdNotEmpty",
        "Product id must not be empty.");
    
}
