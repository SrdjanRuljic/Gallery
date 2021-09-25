using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Gallery.Common.Helpers;
using MediatR;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Commands.UpdatePassword
{
    public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommand, bool>
    {
        private readonly IManagersServices _managersServices;

        public UpdatePasswordCommandHandler(IManagersServices managersServices)
        {
            _managersServices = managersServices;
        }

        public async Task<bool> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
        {
            AppUser user = await _managersServices.FindUserByIdAsync(request.Id);

            if (user == null)
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, ErrorMessages.UserNotFound);

            Result result = await _managersServices.ChangePasswordAsync(user, request.Password);

            if (!result.Succeeded)
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, string.Concat(result.Errors));

            return result.Succeeded;
        }
    }
}
