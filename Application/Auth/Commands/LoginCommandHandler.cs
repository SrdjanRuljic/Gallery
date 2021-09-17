using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Gallery.Common.Helpers;
using MediatR;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Auth.Commands
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, object>
    {

        private readonly IManagersServices _managersServices;
        public readonly IJwtFactory _jwtFactory;

        public LoginCommandHandler(IManagersServices managersServices,
                                   IJwtFactory jwtFactory)
        {
            _managersServices = managersServices;
            _jwtFactory = jwtFactory;
        }

        public async Task<object> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            string errorMessage = null;

            if (!command.IsValid(out errorMessage))
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, errorMessage);

            AppUser user = await _managersServices.Authenticate(command.Username, command.Password);

            if (user == null)
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, ErrorMessages.IncorectUsernameOrPassword);

            string role = await _managersServices.GetRole(user);

            object token = TokenHelper.GenerateJwt(user.UserName, role, _jwtFactory);

            return token;
        }
    }
}
