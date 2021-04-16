using AutoMapper;
using Domain.Entities;
using MediatR;
using Nest;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Products.Queries.Elasticsearch
{
    public class ElasticsearchProductQueryHandler : IRequestHandler<ElasticsearchProductQuery, ElasticsearchProductViewModel>
    {
        private readonly IElasticClient _elasticClient;
        private readonly IMapper _mapper;

        public ElasticsearchProductQueryHandler(IElasticClient elasticClient,
                                                IMapper mapper)
        {
            _elasticClient = elasticClient;
            _mapper = mapper;
        }

        public async Task<ElasticsearchProductViewModel> Handle(ElasticsearchProductQuery request, CancellationToken cancellationToken)
        {
            SearchDescriptor<Product> descriptor = new SearchDescriptor<Product>().TotalHitsAsInteger(true);

            if (!string.IsNullOrEmpty(request.Name))
                descriptor = descriptor.Query(q =>
                    q.QueryString(q =>
                        q.Fields(f =>
                            f.Field(f =>
                                f.Name)).Query("*" + request.Name + "*")));
            else
                descriptor = descriptor.Query(q => q.MatchAll());

            ISearchResponse<Product> response = await _elasticClient.SearchAsync<Product>(
                descriptor.From((request.PageNumber - 1) * request.PageSize)
                          .Size(request.PageSize), cancellationToken);

            return new ElasticsearchProductViewModel
            {
                Products = _mapper.Map<List<ElasticsearchProductQueryResult>>(response.Documents),
                TotalPages = (int)Math.Ceiling(response.Total / (double)request.PageSize),
                PageIndex = request.PageNumber
            };
        }
    }
}
