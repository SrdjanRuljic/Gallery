using Application.Common.Interfaces;
using Application.Common.Pagination;
using Application.Common.Pagination.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Pictures.Commands.Search
{
    public class SearchPicturesCommandHandler : IRequestHandler<SearchPicturesCommand, PaginationResultViewModel<SearchPicturesCommandViewModel>>
    {
        private readonly IGalleryDbContext _context;
        private readonly IMapper _mapper;

        public SearchPicturesCommandHandler(IGalleryDbContext context,
                                    IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginationResultViewModel<SearchPicturesCommandViewModel>> Handle(SearchPicturesCommand command, CancellationToken cancellationToken)
        {
            IQueryable<SearchPicturesCommandViewModel> list = _context.Pictures
                                                                      .Where(x => (string.IsNullOrEmpty(command.Name) ?
                                                                                   true :
                                                                                   x.Name.Contains(command.Name)) &&
                                                                                  (command.CategoryId <= 0 || !command.CategoryId.HasValue ?
                                                                                   true :
                                                                                   x.CategoryId.Equals(command.CategoryId)))
                                                                      .ProjectTo<SearchPicturesCommandViewModel>(_mapper.ConfigurationProvider)
                                                                      .OrderByDescending(x => x.Id);

            PaginatedList<SearchPicturesCommandViewModel> paginatedList = await PaginatedList<SearchPicturesCommandViewModel>.CreateAsync(list,
                                                                                                                                          command.PageNumber,
                                                                                                                                          command.PageSize);

            return new PaginationResultViewModel<SearchPicturesCommandViewModel>
            {
                List = paginatedList,
                PageNumber = paginatedList.PageNumber,
                TotalPages = paginatedList.TotalPages,
                TotalCount = paginatedList.TotalCount
            };
        }
    }
}
