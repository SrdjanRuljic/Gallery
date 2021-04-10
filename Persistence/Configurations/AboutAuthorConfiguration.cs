using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class AboutAuthorConfiguration : IEntityTypeConfiguration<AboutAuthor>
    {
        public void Configure(EntityTypeBuilder<AboutAuthor> builder)
        {
            builder.Property(x => x.Name).IsRequired();
        }
    }
}
