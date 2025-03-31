using SharedKernel;

namespace Domain.Category;

public static class CategoryErrors
{
    public static Error NotFound(Guid categoryId) => Error.NotFound(
        "Categories.NotFound",
        $"The category with the Id = '{categoryId}' was not found");
    public static Error NameNotEmpty() => Error.Failure(
        "Categories.TitleNotEmpty",
        $"The category name can not be empty");
    
    public static Error MinimumStockQuantityInvalid() => Error.Failure(
        "Categories.MinimumStockQuantityInvalid",
        $"The category minimum stock quantity can not be less or equal to zero");
}
