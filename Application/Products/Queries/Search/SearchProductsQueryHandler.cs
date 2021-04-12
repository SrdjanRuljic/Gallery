using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Products.Queries.Search
{
    public class SearchProductsQueryHandler : IRequestHandler<SearchProductsQuery, List<SearchProductsViewModel>>
    {
        private readonly IGalleryDbContext _context;
        private readonly IMapper _mapper;

        public SearchProductsQueryHandler(IGalleryDbContext context,
                                         IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<SearchProductsViewModel>> Handle(SearchProductsQuery command, CancellationToken cancellationToken)
        {
            List<SearchProductsViewModel> list = await _context.Products
                                                              .Where(x => string.IsNullOrEmpty(command.Name) ?
                                                                          true :
                                                                          x.Name.Contains(command.Name))
                                                              .ProjectTo<SearchProductsViewModel>(_mapper.ConfigurationProvider)
                                                              .ToListAsync();

            return list;
        }
    }
}
