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
    public class InsertUserCommandHandler : IRequestHandler<InsertUserCommand>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public InsertUserCommandHandler(UserManager<AppUser> userManager,
                                        RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<Unit> Handle(InsertUserCommand command, CancellationToken cancellationToken)
        {
            string errorMessage = null;

            if (!command.IsValid(out errorMessage))
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, errorMessage);

            command.Username = command.Username.ToLower();

            if (String.IsNullOrEmpty(command.FirstName) || String.IsNullOrWhiteSpace(command.FirstName))
                command.FirstName = null;

            if (String.IsNullOrEmpty(command.LastName) || String.IsNullOrWhiteSpace(command.LastName))
                command.LastName = null;

            AppUser user = await _userManager.FindByNameAsync(command.Username);

            if (user != null)
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, ErrorMessages.UserExists);

            user = new AppUser()
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
                UserName = command.Username
            };

            IdentityResult result = await _userManager.CreateAsync(user);

            if (!result.Succeeded)
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, result.Errors.ToString());

            AppRole role = await _roleManager.FindByIdAsync(command.RoleId);

            result = await _userManager.AddToRoleAsync(user, role.Name);

            if (!result.Succeeded)
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, result.Errors.ToString());

            return Unit.Value;
        }
    }
}
