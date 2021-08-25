using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IGalleryDbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<AboutAuthor> AboutAuthor { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
