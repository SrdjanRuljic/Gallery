using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using Nest;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Elasticsearch.Services
{
    public class FileBlogService : IBlogService
    {
        private readonly List<Product> _cache = new List<Product>();
        private readonly IElasticClient _elasticClient;
        private readonly ILogger _logger;

        public FileBlogService(IElasticClient elasticClient,
                               ILogger logger)
        {
            _elasticClient = elasticClient;
            _logger = logger;
        }

        public async Task DeleteAsync(Product product)
        {
            await _elasticClient.DeleteAsync<Product>(product);

            if (_cache.Contains(product))
                _cache.Remove(product);
        }

        public async Task SaveManyAsync(Product[] products)
        {
            _cache.AddRange(products);
            var response = await _elasticClient.IndexManyAsync(products);
            if (response.Errors)
            {
                // the response can be inspected for errors
                foreach (var itemWithError in response.ItemsWithErrors)
                {
                    _logger.LogError("Failed to index document {0}: {1}",
                        itemWithError.Id, itemWithError.Error);
                }
            }
        }

        public async Task SaveSingleAsync(Product product)
        {
            if (_cache.Any(p => p.Id == product.Id))
            {
                await _elasticClient.UpdateAsync<Product>(product, u => u.Doc(product));
            }
            else
            {
                _cache.Add(product);
                await _elasticClient.IndexDocumentAsync(product);
            }
        }

        public async Task SaveBulkAsync(Product[] products)
        {
            _cache.AddRange(products);
            var response = await _elasticClient.BulkAsync(b => b.Index("products").IndexMany(products));
            if (response.Errors)
            {
                // the response can be inspected for errors
                foreach (var itemWithError in response.ItemsWithErrors)
                {
                    _logger.LogError("Failed to index document {0}: {1}",
                        itemWithError.Id, itemWithError.Error);
                }
            }
        }
    }
}
