using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class CodeConfiguration : IEntityTypeConfiguration<Code>
    {
        public void Configure(EntityTypeBuilder<Code> builder)
        {
            builder.Property(x => x.Name).IsRequired();

            builder.HasOne(d => d.Parent)
                   .WithMany(p => p.Children)
                   .HasForeignKey(d => d.ParentId)
                   .HasConstraintName("FK_Codes_Codes");
        }
    }
}
