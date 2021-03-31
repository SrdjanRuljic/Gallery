using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Pictures.Commands.Search
{
    public class SearchPicturesCommandHandler : IRequestHandler<SearchPicturesCommand, List<SearchPicturesCommandViewModel>>
    {
        private readonly IGalleryDbContext _context;
        private readonly IMapper _mapper;

        public SearchPicturesCommandHandler(IGalleryDbContext context,
                                    IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<SearchPicturesCommandViewModel>> Handle(SearchPicturesCommand command, CancellationToken cancellationToken)
        {
            List<SearchPicturesCommandViewModel> list = await _context.Pictures
                                                              .Where(x => x.Name.Contains(command.Name) &&
                                                                          x.CategoryId.Equals(command.CategoryId)).ProjectTo<SearchPicturesCommandViewModel>(_mapper.ConfigurationProvider)
                                                                                                                  .ToListAsync();

            return list;
        }
    }
}
