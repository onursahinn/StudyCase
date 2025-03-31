using Domain.Category;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Categories;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(t => t.Id);
        builder.HasGeneratedTsVectorColumn(
                c => c.SearchVector,
                "turkish",  // Text search config
                c => c.Name)  // Included property
            .HasIndex(c => c.SearchVector)
            .HasMethod("GIN");
    }
}
