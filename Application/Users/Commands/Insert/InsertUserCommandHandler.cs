using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Gallery.Common.Helpers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Commands.Insert
{
    public class InsertUserCommandHandler : IRequestHandler<InsertUserCommand>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IManagersServices _managersServices;

        public InsertUserCommandHandler(UserManager<AppUser> userManager,
                                        RoleManager<AppRole> roleManager,
                                        IManagersServices managersServices)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _managersServices = managersServices;
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

            AppUser user = await _managersServices.FindByUserNameAsync(command.Username);

            if (user != null)
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, ErrorMessages.UserExists);

            user = new AppUser()
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
                UserName = command.Username
            };

            IdentityResult result = await _managersServices.CreateAsync(command.FirstName, command.LastName, command.Username, command.RoleId);

            if (!result.Succeeded)
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, result.Errors.Select(x => x.Description).ToString());

            return Unit.Value;
        }
    }
}
