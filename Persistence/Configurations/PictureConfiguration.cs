using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class PictureConfiguration : IEntityTypeConfiguration<Picture>
    {
        public void Configure(EntityTypeBuilder<Picture> builder)
        {
            builder.Property(x => x.CategoryId).IsRequired();
            builder.Property(x => x.Name).IsRequired();

            builder.HasOne(d => d.Category)
                   .WithMany(p => p.Pictures)
                   .HasForeignKey(d => d.CategoryId)
                   .HasConstraintName("FK_Pictures_Categories");
        }
    }
}
