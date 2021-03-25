using Application.Common.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.System.Commands.SeedData
{
    public class DataSeeder
    {
        private readonly IGalleryDbContext _context;

        public DataSeeder(IGalleryDbContext context)
        {
            _context = context;
        }

        public async Task SeedAllAsync(CancellationToken cancellationToken)
        {
            if (_context.Roles.Any())
                return;

            await SeedRolesAsync(cancellationToken);
        }

        public async Task SeedRolesAsync(CancellationToken cancellationToken)
        {
            Role admin = new Role()
            {
                Name = "Admin"
            };

            _context.Roles.Add(admin);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
