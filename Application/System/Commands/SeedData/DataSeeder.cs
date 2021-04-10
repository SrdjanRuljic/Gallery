using Application.Common.Behaviours;
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

        public DataSeeder(IGalleryDbContext context)
        {
            _context = context;
        }

        public async Task SeedAllAsync(CancellationToken cancellationToken)
        {
            if (!_context.Roles.Any())
                await SeedRolesAsync(cancellationToken);
            if (!_context.Users.Any())
                await SeedUsersAsync(cancellationToken);
            if (!_context.AboutAuthor.Any())
                await SeedAboutAuthorAsync(cancellationToken);
            else
                return;
        }

        public async Task SeedRolesAsync(CancellationToken cancellationToken)
        {
            Role[] roles = new[]
            {
                new Role()
                {
                    Name = "Admin",
                    Description = "Administratorska uloga"
                },
                new Role()
                {
                    Name = "Moderator",
                    Description = "Moderatorska uloga"
                }
            };

            _context.Roles.AddRange(roles);

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task SeedUsersAsync(CancellationToken cancellationToken)
        {
            byte[] adminPasswordHash;
            byte[] adminPasswordSalt;
            byte[] moderatorPasswordHash;
            byte[] moderatorPasswordSalt;

            Hasher.CreatePasswordHash("Administrator_123!", out adminPasswordHash, out adminPasswordSalt);
            Hasher.CreatePasswordHash("Moderator_123!", out moderatorPasswordHash, out moderatorPasswordSalt);

            User[] users = new[]
            {
                new User()
                {
                    FirstName = "Admin",
                    LastName = "Admin",
                    Username = "admin",
                    RoleId = _context.Roles.FirstOrDefault(x => x.Name == "Admin").Id,
                    PasswordHash = adminPasswordHash,
                    PasswordSalt = adminPasswordSalt

                },
                new User()
                {
                    FirstName = "Moderator",
                    LastName = "Moderator",
                    Username = "moderator",
                    RoleId = _context.Roles.FirstOrDefault(x => x.Name == "Moderator").Id,
                    PasswordHash = moderatorPasswordHash,
                    PasswordSalt = moderatorPasswordSalt
                }
            };

            _context.Users.AddRange(users);

            await _context.SaveChangesAsync(cancellationToken);
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
