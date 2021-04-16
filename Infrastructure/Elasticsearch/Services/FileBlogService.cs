using Application.Common.Interfaces;
using Domain.Entities;
using Nest;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        public async Task<string> SaveManyAsync(Product[] products)
        {
            string message = null;

            _cache.AddRange(products);

            BulkResponse response = await _elasticClient.IndexManyAsync(products);

            if (response.ApiCall.HttpStatusCode != (int)HttpStatusCode.OK)
                message = response.ApiCall.OriginalException.Message;

            if (response.Errors)
            {
                // the response can be inspected for errors
                foreach (var itemWithError in response.ItemsWithErrors)
                {
                    message += "Failed to index document " + itemWithError.Id + ": " + itemWithError.Error;
                }
            }

            return message;
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

        public async Task<string> SaveBulkAsync(Product[] products)
        {
            string message = null;

            _cache.AddRange(products);

            BulkResponse response = await _elasticClient.BulkAsync(b => b.Index("products")
                                                                         .IndexMany(products));

            if (response.ApiCall.HttpStatusCode != (int)HttpStatusCode.OK)
                message = response.ApiCall.OriginalException.Message;

            if (response.Errors)
            {
                // the response can be inspected for errors
                foreach (var itemWithError in response.ItemsWithErrors)
                {
                    message += "Failed to index document " + itemWithError.Id + ": " + itemWithError.Error;
                }
            }

            return message;
        }
    }
}
