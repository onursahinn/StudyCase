using SharedKernel;

namespace Domain.Category;

public static class CategoryErrors
{
    public static Error NotFound(Guid categoryId) => Error.NotFound(
        "Categories.NotFound",
        $"The category with the Id = '{categoryId}' was not found");
}
