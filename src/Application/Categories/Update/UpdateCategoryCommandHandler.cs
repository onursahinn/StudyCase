using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Categories.GetById;
using Application.Products.GetById;
using Domain.Category;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Categories.Update;

public sealed class UpdateCategoryCommandHandler(IApplicationDbContext context) : ICommandHandler<UpdateCategoryCommand,CategoryResponse>
{
    public async Task<Result<CategoryResponse>> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
    {
        Category? category = await context.Categories
            .SingleOrDefaultAsync(t => t.Id == command.CategoryId, cancellationToken);
        if (category is null)
        {
            return Result.Failure<CategoryResponse>(CategoryErrors.NotFound(command.CategoryId));
        }
        category.IsActive = command.IsActive;
        category.Name = command.Name;
        category.MinimumStockQuantity = command.MinimumStockQuantity;
        await context.SaveChangesAsync(cancellationToken);
        return Result.Success(new CategoryResponse()
        {
            Id = category.Id,
            Name = category.Name,
            IsActive = category.IsActive,
            MinimumStockQuantity = category.MinimumStockQuantity,
            CreatedDate = category.CreatedDate,
        });
    }
}
