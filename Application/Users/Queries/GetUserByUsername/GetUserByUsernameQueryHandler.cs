using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Queries.GetUserByUsername
{
    public class GetUserByUsernameQueryHandler : IRequestHandler<GetUserByUsernameQuery, UserLoginDetailsViewModel>
    {
        private readonly IGalleryDbContext _context;
        private readonly IMapper _mapper;

        public GetUserByUsernameQueryHandler(IGalleryDbContext context,
                                             IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserLoginDetailsViewModel> Handle(GetUserByUsernameQuery request, CancellationToken cancellationToken)
        {
            UserLoginDetailsViewModel viewModel = await _context.Users
                                                                .ProjectTo<UserLoginDetailsViewModel>(_mapper.ConfigurationProvider)
                                                                .FirstOrDefaultAsync(x => x.Username.Equals(request.Username));

            return viewModel;
        }
    }
}
