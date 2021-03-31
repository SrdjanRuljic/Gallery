using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Queries.LogedInUserData
{
    public class LogedInUserDataQueryHandler : IRequestHandler<LogedInUserDataQuery, LogedInUserDataViewModel>
    {
        private readonly IGalleryDbContext _context;
        private readonly IMapper _mapper;

        public LogedInUserDataQueryHandler(IGalleryDbContext context,
                                             IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<LogedInUserDataViewModel> Handle(LogedInUserDataQuery request, CancellationToken cancellationToken)
        {
            LogedInUserDataViewModel vievModel = await _context.Users.Where(x => x.Username.Equals(request.Username))
                                                                     .ProjectTo<LogedInUserDataViewModel>(_mapper.ConfigurationProvider)
                                                                     .FirstOrDefaultAsync();

            return vievModel;
        }
    }
}
