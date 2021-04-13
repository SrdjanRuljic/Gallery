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

        public SearchProductsQueryHandler(IGalleryDbContext context,
                                         IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SearchProductsViewModel> Handle(SearchProductsQuery command, CancellationToken cancellationToken)
        {
            //List<SearchProductsViewModel> list = await _context.Products
            //                                                  .Where(x => string.IsNullOrEmpty(command.Name) ?
            //                                                              true :
            //                                                              x.Name.Contains(command.Name))
            //                                                  .ProjectTo<SearchProductsViewModel>(_mapper.ConfigurationProvider)
            //                                                  .ToListAsync();

            var list = _context.Products.Where(x => string.IsNullOrEmpty(command.Name) ?
                                                                         true :
                                                                         x.Name.Contains(command.Name))
                                        .ProjectTo<SearchProductsQueryResult>(_mapper.ConfigurationProvider);

            PaginatedList<SearchProductsQueryResult> products = await PaginatedList<SearchProductsQueryResult>.CreateAsync(list.AsNoTracking(),
                                                                                                                           command.PageNumber ?? 1,
                                                                                                                           command.PageSize);

            return new SearchProductsViewModel
            {
                Products = products,
                TotalPages = products.TotalPages,
                PageIndex = products.PageIndex
            };
        }
    }
}
