using Application.Common.Helpers;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Products.Queries.Search
{
    public class SearchProductsQueryHandler : IRequestHandler<SearchProductsQuery, SearchProductsViewModel>
    {
        private readonly IGalleryDbContext _context;
        private readonly IMapper _mapper;
        private readonly IGalleryMySqlDbContext _myslqContext;

        public SearchProductsQueryHandler(IGalleryDbContext context,
                                         IMapper mapper,
                                         IGalleryMySqlDbContext myslqContext)
        {
            _context = context;
            _mapper = mapper;
            _myslqContext = myslqContext;
        }

        public async Task<SearchProductsViewModel> Handle(SearchProductsQuery command, CancellationToken cancellationToken)
        {
            IQueryable<SearchProductsQueryResult> list = _myslqContext.Products.Where(x => string.IsNullOrEmpty(command.Name) ?
                                                                                           true :
                                                                                           x.Name.Contains(command.Name))
                                                                               .ProjectTo<SearchProductsQueryResult>(_mapper.ConfigurationProvider);

            PaginatedList<SearchProductsQueryResult> paginatedList = await PaginatedList<SearchProductsQueryResult>.CreateAsync(list.AsNoTracking(),
                                                                                                                                command.PageNumber ?? 1,
                                                                                                                                command.PageSize);

            return new SearchProductsViewModel
            {
                Products = paginatedList,
                TotalPages = paginatedList.TotalPages,
                PageIndex = paginatedList.PageIndex
            };
        }
    }
}
