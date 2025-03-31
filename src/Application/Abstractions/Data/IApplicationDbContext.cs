using Domain.Category;
using Domain.Product;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<Product> Products { get; set; }
    DbSet<Category> Categories { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
