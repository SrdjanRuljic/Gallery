using Application.Common.Interfaces;
using Domain;
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

        public DataSeeder(IGalleryDbContext context,
                          RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }

        public async Task SeedAllAsync(CancellationToken cancellationToken)
        {
            if (!_roleManager.Roles.Any())
                await SeedRolesAsync(cancellationToken);
            //if (!_context.Users.Any())
            //    await SeedUsersAsync(cancellationToken);
            if (!_context.AboutAuthor.Any())
                await SeedAboutAuthorAsync(cancellationToken);
            else
                return;
        }

        public async Task SeedRolesAsync(CancellationToken cancellationToken)
        {
            await _roleManager.CreateAsync(new IdentityRole(RoleEnums.Admin.ToString()));
            await _roleManager.CreateAsync(new IdentityRole(RoleEnums.Moderator.ToString()));
        }

        public async Task SeedUsersAsync(CancellationToken cancellationToken)
        {
            //byte[] adminPasswordHash;
            //byte[] adminPasswordSalt;
            //byte[] moderatorPasswordHash;
            //byte[] moderatorPasswordSalt;

            //Hasher.CreatePasswordHash("Administrator_123!", out adminPasswordHash, out adminPasswordSalt);
            //Hasher.CreatePasswordHash("Moderator_123!", out moderatorPasswordHash, out moderatorPasswordSalt);

            //User[] users = new[]
            //{
            //    new User()
            //    {
            //        FirstName = "Admin",
            //        LastName = "Admin",
            //        Username = "admin",
            //        RoleId = _context.Roles.FirstOrDefault(x => x.Name == "Admin").Id,
            //        PasswordHash = adminPasswordHash,
            //        PasswordSalt = adminPasswordSalt

            //    },
            //    new User()
            //    {
            //        FirstName = "Moderator",
            //        LastName = "Moderator",
            //        Username = "moderator",
            //        RoleId = _context.Roles.FirstOrDefault(x => x.Name == "Moderator").Id,
            //        PasswordHash = moderatorPasswordHash,
            //        PasswordSalt = moderatorPasswordSalt
            //    }
            //};

            //_context.Users.AddRange(users);

            //await _context.SaveChangesAsync(cancellationToken);
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
