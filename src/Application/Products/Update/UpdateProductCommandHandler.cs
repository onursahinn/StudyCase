using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Products.GetById;
using Domain.Category;
using Domain.Product;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Products.Update;

public sealed class UpdateProductCommandHandler(IApplicationDbContext context)
    : ICommandHandler<UpdateProductCommand, ProductResponse>
{
    public async Task<Result<ProductResponse>> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        Product? product = await context.Products
            .SingleOrDefaultAsync(t => t.Id == command.ProductId, cancellationToken);
        Category? category = await context.Categories
            .SingleOrDefaultAsync(t => t.Id == command.CategoryId, cancellationToken);
        if (product is null)
        {
            return Result.Failure<ProductResponse>(ProductErrors.NotFound(command.ProductId));
        }

        if (category is null)
        {
            return Result.Failure<ProductResponse>(CategoryErrors.NotFound(command.CategoryId));
        }

        product.Title = command.Title;
        product.Description = command.Description;
        product.StockQuantity = command.StockQuantity;
        product.CategoryId = command.CategoryId;
        SetIsActive(product, category, command.IsActive);
        await context.SaveChangesAsync(cancellationToken);
        return Result.Success(new ProductResponse()
        {
            Id = product.Id,
            Title = product.Title,
            Description = product.Description,
            IsActive = product.IsActive,
            Category = new CategoryLookUp()
            {
                Id = category.Id,
                Name = category.Name,
            },
            StockQuantity = product.StockQuantity,
            CreatedDate = product.CreatedDate,
        });
    }

    private void SetIsActive(Product product, Category category, bool isActive)
    {
        bool stockIsActive = product.StockQuantity >= category.MinimumStockQuantity;
        product.IsActive = isActive && stockIsActive;
    }
}
