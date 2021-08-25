using Application.Common.Behaviours;
using Application.Common.Exceptions;
using Domain.Entities;
using Gallery.Common.Helpers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Commands.Insert
{
    public class InsertUserCommandHandler : IRequestHandler<InsertUserCommand, long>
    {
        private readonly UserManager<User> _userManager;

        public InsertUserCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<long> Handle(InsertUserCommand command, CancellationToken cancellationToken)
        {
            string errorMessage = null;
            byte[] passwordHash;
            byte[] passwordSalt;

            if (!command.IsValid(out errorMessage))
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, errorMessage);

            command.Username = command.Username.ToLower();

            if (String.IsNullOrEmpty(command.FirstName) || String.IsNullOrWhiteSpace(command.FirstName))
                command.FirstName = null;

            if (String.IsNullOrEmpty(command.LastName) || String.IsNullOrWhiteSpace(command.LastName))
                command.LastName = null;

            User user = await _userManager.FindByNameAsync(command.Username);

            if (user != null)
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, ErrorMessages.UserExists);

            Hasher.CreatePasswordHash(command.Password, out passwordHash, out passwordSalt);

            user = new User()
            {
                FirstName = command.FirstName,
                LastName = command.LastName
            };

            await _userManager.CreateAsync(user);

            return 1;
        }
    }
}
