using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IGalleryDbContext
    {
        DbSet<Role> Roles { get; set; }
        DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Picture> Pictures { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
