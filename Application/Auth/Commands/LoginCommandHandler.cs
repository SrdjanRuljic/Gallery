using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Gallery.Common.Helpers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

            var result = await _userManager.Users
                                           .Where(x => x.UserName
                                                        .Equals(command.Username))
                                           .Select(x => new
                                           {
                                               Username = x.UserName,
                                               PasswordHash = x.PasswordHash,
                                               Role = ""
                                           })
                                           .FirstOrDefaultAsync();

            if (result == null)
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, ErrorMessages.IncorectUsernameOrPassword);

            //if (!Hasher.VerifyPassword(command.Password, result.PasswordHash, result.PasswordSalt))
            //    throw new HttpStatusCodeException(HttpStatusCode.BadRequest, ErrorMessages.IncorectUsernameOrPassword);

            object token = TokenHelper.GenerateJwt(result.Username, result.Role, _jwtFactory);

            return token;
        }
    }
}
