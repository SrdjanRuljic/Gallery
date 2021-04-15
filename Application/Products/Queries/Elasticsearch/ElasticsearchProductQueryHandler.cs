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
            ISearchResponse<Product> response = await _elasticClient.SearchAsync<Product>(s =>
                s.Query(q =>
                    q.QueryString(d =>
                        d.Fields(f =>
                            f.Field(f => f.Name.Contains(request.Name)))))
                 .From((request.PageNumber - 1) * request.PageSize)
                 .Size(request.PageSize));

            return new ElasticsearchProductViewModel
            {
                Products = _mapper.Map<List<ElasticsearchProductQueryResult>>(response.Documents),
                TotalPages = (int)Math.Ceiling(response.Total / (double)request.PageSize),
                PageIndex = request.PageNumber
            };
        }
    }
}
