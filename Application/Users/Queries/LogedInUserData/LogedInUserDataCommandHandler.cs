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
    public class LogedInUserDataCommandHandler : IRequestHandler<LogedInUserDataCommand, LogedInUserDataViewModel>
    {
        private readonly IGalleryDbContext _context;
        private readonly IMapper _mapper;

        public LogedInUserDataCommandHandler(IGalleryDbContext context,
                                             IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<LogedInUserDataViewModel> Handle(LogedInUserDataCommand model, CancellationToken cancellationToken)
        {
            LogedInUserDataViewModel vievModel = await _context.Users.Where(x => x.Username.Equals(model.Username))
                                                                     .ProjectTo<LogedInUserDataViewModel>(_mapper.ConfigurationProvider)
                                                                     .FirstOrDefaultAsync();

            return vievModel;
        }
    }
}
