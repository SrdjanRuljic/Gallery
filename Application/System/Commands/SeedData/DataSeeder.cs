using Application.Common.Interfaces;
using Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.System.Commands.SeedData
{
    public class DataSeeder
    {
        private readonly IGalleryDbContext _context;
        private readonly IManagersServices _managersServices;

        public DataSeeder(IGalleryDbContext context,
                          IManagersServices managersServices)
        {
            _context = context;
            _managersServices = managersServices;
        }

        public async Task SeedAllAsync(CancellationToken cancellationToken)
        {
            if (!await _managersServices.IsThereAnyRoleAsync())
                await SeedRolessAsync();
            if (!await _managersServices.IsThereAnyUserAsync())
                await SeedUsersAsync();
            if (!_context.AboutAuthor.Any())
                await SeedAboutAuthorAsync(cancellationToken);
            else
                return;
        }

        public async Task SeedRolessAsync()
        {
            await DefaultRoles.SeedAsync(_managersServices);
        }

        public async Task SeedUsersAsync()
        {
            await DefaultUsers.Admin.SeedAsync(_managersServices);
            await DefaultUsers.Moderator.SeedAsync(_managersServices);
        }

        public async Task SeedAboutAuthorAsync(CancellationToken cancellationToken)
        {
            AboutAuthor aboutAuthor = new AboutAuthor()
            {
                Name = "Dragana Kezunović",
                Biography = "U izradi"
            };

            _context.AboutAuthor.Add(aboutAuthor);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
