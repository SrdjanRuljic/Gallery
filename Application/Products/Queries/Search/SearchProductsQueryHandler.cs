using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Nest;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Products.Queries.Search
{
    public class SearchProductsQueryHandler : IRequestHandler<SearchProductsQuery, SearchProductsViewModel>
    {
        private readonly IGalleryDbContext _context;
        private readonly IMapper _mapper;
        private readonly IGalleryMySqlDbContext _myslqContext;
        private readonly IElasticClient _elasticClient;

        public SearchProductsQueryHandler(IGalleryDbContext context,
                                         IMapper mapper,
                                         IGalleryMySqlDbContext myslqContext,
                                         IElasticClient elasticClient)
        {
            _context = context;
            _mapper = mapper;
            _myslqContext = myslqContext;
            _elasticClient = elasticClient;
        }

        public async Task<SearchProductsViewModel> Handle(SearchProductsQuery command, CancellationToken cancellationToken)
        {
            ISearchResponse<Product> response = await _elasticClient.SearchAsync<Product>(
                s => s.Query(q => q.QueryString(d => d.Query(command.Name))));

            //IQueryable<SearchProductsQueryResult> list = _myslqContext.Products.Where(x => string.IsNullOrEmpty(command.Name) ?
            //                                                                               true :
            //                                                                               x.Name.Contains(command.Name))
            //                                                                   .ProjectTo<SearchProductsQueryResult>(_mapper.ConfigurationProvider);

            //PaginatedList<SearchProductsQueryResult> paginatedList = await PaginatedList<SearchProductsQueryResult>.CreateAsync(list.AsNoTracking(),
            //                                                                                                                    command.PageNumber ?? 1,
            //                                                                                                                    command.PageSize);

            return new SearchProductsViewModel
            {
                Products = _mapper.Map<List<SearchProductsQueryResult>>(response.Documents),
                TotalPages = response.Total,
                PageIndex = command.PageNumber
            };
        }
    }
}
