using Application.Common.Exceptions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Gallery.Common.Helpers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Queries.GetById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, GetUserByIdViewModel>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public GetUserByIdQueryHandler(UserManager<User> userManager,
                                       IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<GetUserByIdViewModel> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.Id <= 0)
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, ErrorMessages.IdCanNotBeLowerThanOne);


            GetUserByIdViewModel model = await _userManager.Users
                                                       .ProjectTo<GetUserByIdViewModel>(_mapper.ConfigurationProvider)
                                                       .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (model == null)
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, ErrorMessages.UserNotFound);

            return model;
        }
    }
}
