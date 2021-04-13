using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.CategoryId).IsRequired();
            builder.Property(x => x.Name).IsRequired()
                                         .HasMaxLength(120);

            builder.HasOne(d => d.Category)
                   .WithMany(p => p.Products)
                   .HasForeignKey(d => d.CategoryId)
                   .HasConstraintName("FK_Products_Categories");

            builder.HasIndex(e => new
            {
                e.Name
            }).HasName("IX_NameIndex")
              .IsUnique();
        }
    }
}
