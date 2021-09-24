using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Gallery.Common.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Queries.LogedInUserData
{
    public class LogedInUserDataQueryHandler : IRequestHandler<LogedInUserDataQuery, LogedInUserDataViewModel>
    {
        private readonly IManagersServices _managersServices;
        private readonly IMapper _mapper;

        public LogedInUserDataQueryHandler(IManagersServices managersServices,
                                           IMapper mapper)
        {
            _managersServices = managersServices;
            _mapper = mapper;
        }

        public async Task<LogedInUserDataViewModel> Handle(LogedInUserDataQuery request, CancellationToken cancellationToken)
        {
            LogedInUserDataViewModel vievModel = await _managersServices.FindByUserName(request.Username)
                                                                        .ProjectTo<LogedInUserDataViewModel>(_mapper.ConfigurationProvider)
                                                                        .FirstOrDefaultAsync();

            if (vievModel == null)
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, ErrorMessages.DataNotFound);

            return vievModel;
        }
    }
}
