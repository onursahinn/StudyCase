using Domain.Category;
using Domain.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Products;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(t => t.Id);
        builder.HasOne<Category>().WithMany().HasForeignKey(t => t.CategoryId);
        builder.Property(t => t.Title).HasMaxLength(200);
    }
}
