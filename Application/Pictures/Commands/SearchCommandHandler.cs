using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Pictures.Commands
{
    public class SearchCommandHandler : IRequestHandler<SearchCommand, List<SearchCommandViewModel>>
    {
        private readonly IGalleryDbContext _context;
        private readonly IMapper _mapper;

        public SearchCommandHandler(IGalleryDbContext context,
                                    IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<SearchCommandViewModel>> Handle(SearchCommand request, CancellationToken cancellationToken)
        {
            List<SearchCommandViewModel> list = await _context.Pictures
                                                              .Where(x => x.Name.Contains(request.Name) &&
                                                                          x.CategoryId.Equals(request.CategoryId)).ProjectTo<SearchCommandViewModel>(_mapper.ConfigurationProvider)
                                                                                                                  .ToListAsync();

            return list;
        }
    }
}
