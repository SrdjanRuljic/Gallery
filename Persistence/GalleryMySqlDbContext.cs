using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Persistence
{
    public class GalleryMySqlDbContext : DbContext, IGalleryMySqlDbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Code> Codes { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<CodeLanguage> CodesLanguages { get; set; }

        public GalleryMySqlDbContext(DbContextOptions<GalleryMySqlDbContext> options)
            : base(options)
        {

        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GalleryMySqlDbContext).Assembly);
        }

    }
}
