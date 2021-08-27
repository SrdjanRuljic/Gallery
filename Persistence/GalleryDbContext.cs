using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Persistence
{
    public class GalleryDbContext : IdentityDbContext<AppUser,
                                                      AppRole,
                                                      string,
                                                      IdentityUserClaim<string>,
                                                      AppUserRole,
                                                      IdentityUserLogin<string>,
                                                      IdentityRoleClaim<string>,
                                                      IdentityUserToken<string>>, IGalleryDbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<AboutAuthor> AboutAuthor { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        public GalleryDbContext(DbContextOptions<GalleryDbContext> options)
            : base(options)
        {

        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GalleryDbContext).Assembly);
        }
    }
}
