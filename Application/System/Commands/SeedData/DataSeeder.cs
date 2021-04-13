using Application.Common.Behaviours;
using Application.Common.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.System.Commands.SeedData
{
    public class DataSeeder
    {
        private readonly IGalleryDbContext _context;
        private static Random _random = new Random();

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
            if (!_context.Products.Any() || _context.Products.Count() < 200000)
                await SeedProductsAsync(cancellationToken);
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

        public async Task SeedProductsAsync(CancellationToken cancellationToken)
        {
            List<Product> products = new List<Product>();

            for (int i = 0; i < 100000; i++)
            {
                Product product = new Product()
                {
                    Name = RandomString(100),
                    CategoryId = 1,
                    Description = null,
                    Content = RandomString(500),
                    Extension = RandomString(3)
                };

                products.Add(product);
            }

            for (int i = 0; i < 100000; i++)
            {
                Product product = new Product()
                {
                    Name = RandomString(100),
                    CategoryId = 10008,
                    Description = null,
                    Content = RandomString(500),
                    Extension = RandomString(3)
                };

                products.Add(product);
            }

            _context.Products.AddRange(products);

            await _context.SaveChangesAsync(cancellationToken);
        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 ";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[_random.Next(s.Length)]).ToArray());
        }
    }
}
