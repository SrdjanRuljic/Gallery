using Application.Common.Interfaces;
using Application.Common.Pagination;
using Application.Common.Pagination.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Queries.GetAll
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, PaginationResultViewModel<GetAllUsersViewModel>>
    {
        private readonly IManagersServices _managersServices;
        private readonly IMapper _mapper;

        public GetAllUsersQueryHandler(IManagersServices managersServices,
                                       IMapper mapper)
        {
            _managersServices = managersServices;
            _mapper = mapper;
        }

        public async Task<PaginationResultViewModel<GetAllUsersViewModel>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            IQueryable<GetAllUsersViewModel> list = _managersServices.GetAllUsers()
                                                                     .ProjectTo<GetAllUsersViewModel>(_mapper.ConfigurationProvider);

            PaginatedList<GetAllUsersViewModel> paginatedList = await PaginatedList<GetAllUsersViewModel>.CreateAsync(list,
                                                                                                                      request.PageNumber,
                                                                                                                      request.PageSize);

            return new PaginationResultViewModel<GetAllUsersViewModel>
            {
                List = paginatedList,
                PageNumber = paginatedList.PageNumber,
                TotalPages = paginatedList.TotalPages,
                TotalCount = paginatedList.TotalCount
            };
        }
    }
}
