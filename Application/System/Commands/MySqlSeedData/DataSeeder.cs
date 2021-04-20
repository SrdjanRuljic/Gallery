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
            if (!_context.Languages.Any())
                await SeedLanguagesAsync(cancellationToken);
            if (!_context.Codes.Any())
                await SeedCodesAsync(cancellationToken);
            if (!_context.CodesLanguages.Any())
                await SeedCodesLanguagesAsync(cancellationToken);
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

            string message = await _blogService.SaveManyAsync(products);
        }

        public async Task SeedLanguagesAsync(CancellationToken cancellationToken)
        {

            Language[] languages = new[]
            {
                new Language()
                {
                    Name = "Engleski"
                },
                new Language()
                {
                    Name = "Srpski"
                },
                new Language()
                {
                    Name = "Crnski"
                },
                new Language()
                {
                    Name = "Gorski"
                },
            };

            _context.Languages.AddRange(languages);

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task SeedCodesAsync(CancellationToken cancellationToken)
        {
            for (int i = 0; i <= 14635; i++)
            {
                Code code = new Code();

                if (i <= 21)
                {
                    code.Name = RandomString(100);
                    code.ParentId = null;
                }
                else if (i > 21 && i <= 118)
                {
                    code.Name = RandomString(100);
                    code.ParentId = RandomLong(1, 21);
                }
                else if (i > 118 && i <= 1373)
                {
                    code.Name = RandomString(100);
                    code.ParentId = RandomLong(21, 118);
                }
                else if (i > 1373 && i <= 4844)
                {
                    code.Name = RandomString(100);
                    code.ParentId = RandomLong(118, 1373);
                }
                else if (i > 4844 && i <= 9653)
                {
                    code.Name = RandomString(100);
                    code.ParentId = RandomLong(1373, 4844);
                }
                else if (i > 9653 && i <= 12472)
                {
                    code.Name = RandomString(100);
                    code.ParentId = RandomLong(4844, 9653);
                }
                else if (i > 12472 && i <= 13678)
                {
                    code.Name = RandomString(100);
                    code.ParentId = RandomLong(9653, 12472);
                }
                else if (i > 13678 && i <= 14236)
                {
                    code.Name = RandomString(100);
                    code.ParentId = RandomLong(12472, 13678);
                }
                else if (i > 14236 && i <= 14440)
                {
                    code.Name = RandomString(100);
                    code.ParentId = RandomLong(13678, 14236);
                }
                else if (i > 14440 && i <= 14572)
                {
                    code.Name = RandomString(100);
                    code.ParentId = RandomLong(14236, 14440);
                }
                else if (i > 14572 && i <= 14635)
                {
                    code.Name = RandomString(100);
                    code.ParentId = RandomLong(14440, 14572);
                }

                _context.Codes.Add(code);

                await _context.SaveChangesAsync(cancellationToken);
            }


        }

        public async Task SeedCodesLanguagesAsync(CancellationToken cancellationToken)
        {
            CodeLanguage[] codesLanguages = new CodeLanguage[103000];

            for (int i = 0; i < 103000; i++)
            {
                CodeLanguage codeLanguage = new CodeLanguage()
                {
                    CodeId = RandomLong(1, 14635),
                    LanguageId = RandomLong(1, 4),
                    Translation = RandomString(100)
                };

                codesLanguages[i] = codeLanguage;
            }

            _context.CodesLanguages.AddRange(codesLanguages);

            await _context.SaveChangesAsync(cancellationToken);
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
