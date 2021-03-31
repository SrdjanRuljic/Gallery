using Application.Common.Behaviours;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Gallery.Common.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Auth.Commands
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, object>
    {

        private readonly IGalleryDbContext _context;
        public readonly IJwtFactory _jwtFactory;

        public LoginCommandHandler(IGalleryDbContext context,
                                   IJwtFactory jwtFactory)
        {
            _context = context;
            _jwtFactory = jwtFactory;
        }

        public async Task<object> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            string errorMessage = null;

            if (!command.IsValid(out errorMessage))
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, errorMessage);

            var result = await _context.Users.Where(x => x.Username
                                                          .Equals(command.Username))
                                             .Select(x => new
                                             {
                                                 Username = x.Username,
                                                 PasswordHash = x.PasswordHash,
                                                 PasswordSalt = x.PasswordSalt,
                                                 Role = x.Role.Name
                                             })
                                             .FirstOrDefaultAsync();

            if (result == null)
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, ErrorMessages.IncorectUsernameOrPassword);

            if (!Hasher.VerifyPassword(command.Password, result.PasswordHash, result.PasswordSalt))
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, ErrorMessages.IncorectUsernameOrPassword);

            object token = TokenHelper.GenerateJwt(result.Username, result.Role, _jwtFactory);

            return token;
        }
    }
}
