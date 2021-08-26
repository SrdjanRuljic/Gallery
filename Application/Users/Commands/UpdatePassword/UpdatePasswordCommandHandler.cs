using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Commands.UpdatePassword
{
    public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommand, bool>
    {
        private readonly UserManager<AppUser> _userManager;

        public UpdatePasswordCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
        {
            AppUser user = await _userManager.FindByIdAsync(request.Id);

            await _userManager.ChangePasswordAsync(user, user.PasswordHash, request.Password);

            return true;
        }
    }
}
