using Application.Common.Exceptions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Gallery.Common.Helpers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Queries.LogedInUserData
{
    public class LogedInUserDataQueryHandler : IRequestHandler<LogedInUserDataQuery, LogedInUserDataViewModel>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public LogedInUserDataQueryHandler(UserManager<AppUser> userManager,
                                       IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<LogedInUserDataViewModel> Handle(LogedInUserDataQuery request, CancellationToken cancellationToken)
        {
            LogedInUserDataViewModel vievModel = await _userManager.Users.Where(x => x.UserName.Equals(request.Username))
                                                                     .ProjectTo<LogedInUserDataViewModel>(_mapper.ConfigurationProvider)
                                                                     .FirstOrDefaultAsync();

            if (vievModel == null)
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, ErrorMessages.DataNotFound);

            return vievModel;
        }
    }
}
