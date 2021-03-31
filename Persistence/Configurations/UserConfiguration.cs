using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.RoleId).IsRequired();
            builder.Property(x => x.Username).IsRequired();
            builder.Property(x => x.PasswordHash).IsRequired();
            builder.Property(x => x.PasswordSalt).IsRequired();

            builder.HasOne(d => d.Role)
                   .WithMany(p => p.Users)
                   .HasForeignKey(d => d.RoleId)
                   .HasConstraintName("FK_Users_Roles");
        }
    }
}
