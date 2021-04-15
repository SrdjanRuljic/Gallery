using Application.Common.Interfaces;
using Domain.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.System.Commands.MySqlSeedData
{
    public class DataSeeder
    {
        private readonly IGalleryMySqlDbContext _context;
        private static Random _random = new Random();
        private readonly IBlogService _blogService;

        public DataSeeder(IGalleryMySqlDbContext context,
                          IBlogService blogService)
        {
            _context = context;
            _blogService = blogService;
        }

        public async Task SeedAllAsync(CancellationToken cancellationToken)
        {
            if (!_context.Categories.Any())
                await SeedCategoriesAsync(cancellationToken);
            if (!_context.Products.Any())
                await SeedProductsAsync(cancellationToken);
            else
                return;
        }

        public async Task SeedCategoriesAsync(CancellationToken cancellationToken)
        {
            Category[] categories = new[]
            {
                new Category()
                {
                    Name = "Portreti"
                },
                new Category()
                {
                    Name = "Pejzazi"
                }
            };

            _context.Categories.AddRange(categories);

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task SeedProductsAsync(CancellationToken cancellationToken)
        {
            Product[] products = new Product[100000];

            for (int i = 0; i < 100000; i++)
            {
                Product product = new Product()
                {
                    Name = RandomString(100),
                    CategoryId = RandomLong(1, 2),
                    Description = null,
                    Content = RandomString(500),
                    Extension = RandomString(3)
                };

                products[i] = product;
            }

            _context.Products.AddRange(products);

            await _context.SaveChangesAsync(cancellationToken);

            await _blogService.SaveManyAsync(products);
        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 ";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[_random.Next(s.Length)]).ToArray());
        }

        public static long RandomLong(int min, int max)
        {
            return (long)_random.Next(min, max);
        }
    }
}
