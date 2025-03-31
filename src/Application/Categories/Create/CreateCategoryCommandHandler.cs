using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Category;
using SharedKernel;

namespace Application.Categories.Create;

public sealed class CreateCategoryCommandHandler(IApplicationDbContext context, IDateTimeProvider dateTimeProvider) : ICommandHandler<CreateCategoryCommand,Guid>
{
    public async Task<Result<Guid>> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
    {
        var category = new Category()
        {
            Name = command.Name,
            MinimumStockQuantity = command.MinimumStockQuantity,
            IsDeleted = false,
            CreatedDate = dateTimeProvider.UtcNow
        };
        context.Categories.Add(category);
        await context.SaveChangesAsync(cancellationToken);
        return category.Id;
    }
}
