using Application.Common.Interfaces;
using Domain.Entities;
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

        public FileBlogService(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public async Task DeleteAsync(Product product)
        {
            await _elasticClient.DeleteAsync<Product>(product);

            if (_cache.Contains(product))
                _cache.Remove(product);
        }

        public async Task<bool> SaveManyAsync(Product[] products)
        {
            _cache.AddRange(products);

            BulkResponse result = await _elasticClient.IndexManyAsync(products);

            if (result.Errors)
                return false;

            return true;
        }

        public async Task SaveSingleAsync(Product product)
        {
            if (_cache.Any(p => p.Id == product.Id))
                await _elasticClient.UpdateAsync<Product>(product, u => u.Doc(product));

            else
            {
                _cache.Add(product);
                await _elasticClient.IndexDocumentAsync(product);
            }
        }
    }
}
