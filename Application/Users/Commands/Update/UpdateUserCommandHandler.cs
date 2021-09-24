using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Gallery.Common.Helpers;
using MediatR;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Commands.Update
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IManagersServices _managersServices;

        public UpdateUserCommandHandler(IManagersServices managersServices)
        {
            _managersServices = managersServices;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            string errorMessage = null;

            if (!request.IsValid(out errorMessage))
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, errorMessage);

            //Current
            AppUser userEntity = await _managersServices.FindUserByIdAsync(request.Id);

            if (userEntity == null)
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, ErrorMessages.UserNotFound);

            request.Username = request.Username.ToLower();

            if (String.IsNullOrEmpty(request.FirstName) || String.IsNullOrWhiteSpace(request.FirstName))
                request.FirstName = null;

            if (String.IsNullOrEmpty(request.LastName) || String.IsNullOrWhiteSpace(request.LastName))
                request.LastName = null;

            //Update user
            AppUser user = await _managersServices.FindByUserNameAsync(request.Username);

            if (request.Username != userEntity.UserName && user != null)
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, ErrorMessages.UserExists);

            Result result = await _managersServices.UpdateUserAsync(userEntity,
                                                                    request.FirstName,
                                                                    request.LastName,
                                                                    request.Username,
                                                                    request.RoleId);

            if (!result.Succeeded)
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, string.Concat(result.Errors));

            return Unit.Value;
        }
    }
}
