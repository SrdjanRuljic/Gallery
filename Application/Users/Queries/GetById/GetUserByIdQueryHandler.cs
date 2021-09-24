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

namespace Application.Users.Queries.GetById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, GetUserByIdViewModel>
    {
        private readonly IManagersServices _managersServices;
        private readonly IMapper _mapper;

        public GetUserByIdQueryHandler(IManagersServices managersServices,
                                       IMapper mapper)
        {
            _managersServices = managersServices;
            _mapper = mapper;
        }

        public async Task<GetUserByIdViewModel> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            GetUserByIdViewModel model = await _managersServices.FindUserById(request.Id)
                                                                .ProjectTo<GetUserByIdViewModel>(_mapper.ConfigurationProvider)
                                                                .FirstOrDefaultAsync();

            if (model == null)
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, ErrorMessages.UserNotFound);

            return model;
        }
    }
}
