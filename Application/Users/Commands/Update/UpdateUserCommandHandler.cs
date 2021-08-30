using Application.Common.Exceptions;
using Domain.Entities;
using Gallery.Common.Helpers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Commands.Update
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public UpdateUserCommandHandler(UserManager<AppUser> userManager,
                                        RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            string errorMessage = null;

            if (!request.IsValid(out errorMessage))
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, errorMessage);

            //Current
            AppUser userEntity = await _userManager.FindByIdAsync(request.Id);
            IList<string> userRoles = await _userManager.GetRolesAsync(userEntity);
            AppRole currentRole = await _roleManager.FindByNameAsync(userRoles.FirstOrDefault());

            if (userEntity == null)
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, ErrorMessages.UserNotFound);

            request.Username = request.Username.ToLower();

            if (String.IsNullOrEmpty(request.FirstName) || String.IsNullOrWhiteSpace(request.FirstName))
                request.FirstName = null;

            if (String.IsNullOrEmpty(request.LastName) || String.IsNullOrWhiteSpace(request.LastName))
                request.LastName = null;

            //Update user
            AppUser user = await _userManager.FindByNameAsync(request.Username);

            if (request.Username != userEntity.UserName && user != null)
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, ErrorMessages.UserExists);

            userEntity.FirstName = request.FirstName;
            userEntity.LastName = request.LastName;
            userEntity.UserName = request.Username;

            IdentityResult result = await _userManager.UpdateAsync(userEntity);

            if (!result.Succeeded)
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, result.Errors.Select(x => x.Description).ToString());

            // Update User's role
            AppRole newRole = await _roleManager.FindByIdAsync(request.RoleId);

            if (!request.RoleId.Equals(currentRole.Id))
            {
                result = await _userManager.RemoveFromRolesAsync(userEntity, userRoles);

                if (!result.Succeeded)
                    throw new HttpStatusCodeException(HttpStatusCode.BadRequest, result.Errors.Select(x => x.Description).ToString());

                result = await _userManager.AddToRoleAsync(userEntity, newRole.Name);

                if (!result.Succeeded)
                    throw new HttpStatusCodeException(HttpStatusCode.BadRequest, result.Errors.Select(x => x.Description).ToString());
            }

            return Unit.Value;
        }
    }
}
