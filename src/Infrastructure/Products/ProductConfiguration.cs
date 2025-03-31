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
        builder.Property(t => t.Title).HasMaxLength(200);
        
        // Configure the full-text search vector
        builder.HasGeneratedTsVectorColumn(
                p => p.SearchVector,
                "turkish",  // Text search config
                p => p.SearchText)  // Included property
            .HasIndex(p => p.SearchVector)
            .HasMethod("GIN");
    }
}
