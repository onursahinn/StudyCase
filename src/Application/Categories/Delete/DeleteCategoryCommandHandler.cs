using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Category;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Categories.Delete;

public sealed class DeleteCategoryCommandHandler(IApplicationDbContext context) : ICommandHandler<DeleteCategoryCommand>
{
    public async Task<Result> Handle(DeleteCategoryCommand command, CancellationToken cancellationToken)
    {
        Category? category = await context.Categories
            .SingleOrDefaultAsync(t => t.Id == command.CategoryId, cancellationToken);

        if (category is null)
        {
            return Result.Failure(CategoryErrors.NotFound(command.CategoryId));
        }

        category.IsDeleted = true;
        
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
