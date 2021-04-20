using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IGalleryMySqlDbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<Code> Codes { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<CodeLanguage> CodesLanguages { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
