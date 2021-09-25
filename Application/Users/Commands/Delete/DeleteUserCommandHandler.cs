using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Gallery.Common.Helpers;
using MediatR;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Commands.Delete
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IManagersServices _managersServices;

        public DeleteUserCommandHandler(IManagersServices managersServices)
        {
            _managersServices = managersServices;
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            AppUser user = await _managersServices.FindUserByIdAsync(request.Id);

            if (user == null)
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, ErrorMessages.CategoryNotFound);

            await _managersServices.DeleteUserAsync(user);

            return Unit.Value;
        }
    }
}
