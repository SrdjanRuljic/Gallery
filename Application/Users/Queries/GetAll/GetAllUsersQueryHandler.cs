using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Queries.GetAll
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<GetAllUsersViewModel>>
    {
        private readonly IGalleryDbContext _context;
        private readonly IMapper _mapper;

        public GetAllUsersQueryHandler(IGalleryDbContext context,
                                       IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<GetAllUsersViewModel>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            List<GetAllUsersViewModel> list = await _context.Users
                                                            .ProjectTo<GetAllUsersViewModel>(_mapper.ConfigurationProvider)
                                                            .ToListAsync();

            return list;
        }
    }
}
