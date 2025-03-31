using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Product;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Products.Delete;

public sealed class DeleteProductCommandHandler(IApplicationDbContext context) : ICommandHandler<DeleteProductCommand>
{
    public async Task<Result> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        Product? product = await context.Products
            .SingleOrDefaultAsync(t => t.Id == command.ProductId, cancellationToken);

        if (product is null)
        {
            return Result.Failure(ProductErrors.NotFound(command.ProductId));
        }

        product.IsDeleted = true;
        
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
