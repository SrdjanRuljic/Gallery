using Application.Common.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.System.Commands.MySqlSeedData
{
    public class DataSeeder
    {
        private readonly IGalleryMySqlDbContext _context;
        private static Random _random = new Random();

        public DataSeeder(IGalleryMySqlDbContext context)
        {
            _context = context;
        }

        public async Task SeedAllAsync(CancellationToken cancellationToken)
        {
            if (!_context.Categories.Any())
                await SeedCategoriesAsync(cancellationToken);
            if (!_context.Products.Any() || _context.Products.Count() < 200000)
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
                    CategoryId = 2,
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
