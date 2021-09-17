using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class AppUserRoleConfiguration : IEntityTypeConfiguration<AppUserRole>
    {
        public void Configure(EntityTypeBuilder<AppUserRole> builder)
        {
            builder.HasOne(d => d.User)
                   .WithMany(p => p.UserRoles)
                   .HasForeignKey(d => d.UserId)
                   .IsRequired();

            builder.HasOne(d => d.Role)
                   .WithMany(p => p.UserRoles)
                   .HasForeignKey(d => d.RoleId)
                   .IsRequired();
        }
    }
}
