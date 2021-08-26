using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Gallery.Common.Helpers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Auth.Commands
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, object>
    {

        private readonly UserManager<AppUser> _userManager;
        public readonly IJwtFactory _jwtFactory;

        public LoginCommandHandler(UserManager<AppUser> userManager,
                                   IJwtFactory jwtFactory)
        {
            _userManager = userManager;
            _jwtFactory = jwtFactory;
        }

        public async Task<object> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            string errorMessage = null;

            if (!command.IsValid(out errorMessage))
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, errorMessage);

            AppUser user = await _userManager.FindByNameAsync(command.Username);

            if (user == null)
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, ErrorMessages.IncorectUsernameOrPassword);

            IList<string> roles = await _userManager.GetRolesAsync(user);

            object token = TokenHelper.GenerateJwt(user.UserName, roles.FirstOrDefault(), _jwtFactory);

            return token;
        }
    }
}
