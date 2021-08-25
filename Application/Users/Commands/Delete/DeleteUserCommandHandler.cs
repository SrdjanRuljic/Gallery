using Application.Common.Exceptions;
using Domain.Entities;
using Gallery.Common.Helpers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Commands.Delete
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly UserManager<User> _userManager;

        public DeleteUserCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            User user = await _userManager.FindByIdAsync(request.Id);

            if (user == null)
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, ErrorMessages.CategoryNotFound);

            await _userManager.DeleteAsync(user);

            return Unit.Value;
        }
    }
}
