using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using System;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace Infrastructure.Elasticsearch
{
    public static class ElasticsearchExtensions
    {
        public static void AddElasticsearch(this IServiceCollection services, IConfiguration configuration)
        {
            string url = configuration["elasticsearch:url"];
            string defaultIndex = configuration["elasticsearch:index"];

            ConnectionSettings settings = new ConnectionSettings(new Uri(url)).DefaultIndex(defaultIndex);

            AddDefaultMappings(settings);

            ElasticClient client = new ElasticClient(settings);

            services.AddSingleton<IElasticClient>(client);

            CreateIndex(client, defaultIndex);
        }

        private static void AddDefaultMappings(ConnectionSettings settings)
        {
            settings.DefaultMappingFor<Product>(m => m.Ignore(p => p.Content)
                                                      .Ignore(p => p.Description)
                                                      .Ignore(p => p.Extension));
        }

        private static void CreateIndex(IElasticClient client, string indexName)
        {
            CreateIndexResponse createIndexResponse = client.Indices.Create(indexName, index => index.Map<Product>(x => x.AutoMap())
        );
        }
    }
}
