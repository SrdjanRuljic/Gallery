using Application.Common.Interfaces;
using Application.Common.Pagination;
using Application.Common.Pagination.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Categories.Queries.GetAll
{
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, PaginationResultViewModel<GetAllCategoriesViewModel>>
    {
        private readonly IGalleryDbContext _context;
        private readonly IMapper _mapper;

        public GetAllCategoriesQueryHandler(IGalleryDbContext context,
                                            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginationResultViewModel<GetAllCategoriesViewModel>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            IQueryable<GetAllCategoriesViewModel> list = _context.Categories
                                                                 .ProjectTo<GetAllCategoriesViewModel>(_mapper.ConfigurationProvider);

            PaginatedList<GetAllCategoriesViewModel> paginatedList = await PaginatedList<GetAllCategoriesViewModel>.CreateAsync(list,
                                                                                                                                request.PageNumber,
                                                                                                                                request.PageSize);


            return new PaginationResultViewModel<GetAllCategoriesViewModel>
            {
                List = paginatedList,
                PageNumber = paginatedList.PageNumber,
                TotalPages = paginatedList.TotalPages,
                TotalCount = paginatedList.TotalCount
            };
        }
    }
}
