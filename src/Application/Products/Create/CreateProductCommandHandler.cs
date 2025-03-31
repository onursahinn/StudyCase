using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Category;
using Domain.Product;
using SharedKernel;

namespace Application.Products.Create;

public sealed class CreateProductCommandHandler(IApplicationDbContext context, IDateTimeProvider dateTimeProvider)
    : ICommandHandler<CreateProductCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        Category? category = context.Categories.SingleOrDefault(x => x.Id == command.CategoryId);
        if (category is null)
        {
            return Result.Failure<Guid>(CategoryErrors.NotFound(command.CategoryId));
        }

        var product = new Product()
        {
            Description = command.Description,
            Title = command.Title,
            CategoryId = command.CategoryId,
            StockQuantity = command.StockQuantity,
            IsDeleted = false,
            CreatedDate = dateTimeProvider.UtcNow
        };
        SetIsActive(product, category);

        context.Products.Add(product);

        await context.SaveChangesAsync(cancellationToken);

        return product.Id;
    }

    private void SetIsActive(Product product, Category category)
    {
        product.IsActive = product.StockQuantity >= category.MinimumStockQuantity;
    }
}
