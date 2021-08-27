using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.System.Commands.SeedData
{
    public class DataSeeder
    {
        private readonly IGalleryDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public DataSeeder(IGalleryDbContext context,
                          RoleManager<IdentityRole> roleManager,
                          UserManager<AppUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task SeedAllAsync(CancellationToken cancellationToken)
        {
            if (!_roleManager.Roles.Any())
                await SeedRolessAsync();
            if (!_userManager.Users.Any())
                await SeedUsersAsync();
            if (!_context.AboutAuthor.Any())
                await SeedAboutAuthorAsync(cancellationToken);
            else
                return;
        }

        public async Task SeedRolessAsync()
        {
            await DefaultRoles.SeedAsync(_roleManager);
        }

        public async Task SeedUsersAsync()
        {
            await DefaultUsers.Admin.SeedAsync(_userManager);
            await DefaultUsers.Moderator.SeedAsync(_userManager);
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
