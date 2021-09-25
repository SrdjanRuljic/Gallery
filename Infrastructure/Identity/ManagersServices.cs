using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class ManagersServices : IManagersServices
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;

        public ManagersServices(UserManager<AppUser> userManager,
                                SignInManager<AppUser> signInManager,
                                RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<AppUser> AuthenticateAsync(string username, string password)
        {
            AppUser user = new AppUser();

            SignInResult result = await _signInManager.PasswordSignInAsync(username, password, false, lockoutOnFailure: false);

            if (result.Succeeded)
                user = await _userManager.FindByNameAsync(username);

            return user;
        }

        public async Task CreateRoleAsync(string roleName) =>
            await _roleManager.CreateAsync(new AppRole()
            {
                Name = roleName
            });

        public async Task<(Result Result, string UserId)> CreateUserAsync(string firstName,
                                                                          string lastName,
                                                                          string userName,
                                                                          string password,
                                                                          string roleId = null,
                                                                          string roleName = null)
        {
            AppUser user = new AppUser()
            {
                FirstName = firstName,
                LastName = lastName,
                UserName = userName
            };

            IdentityResult result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
                return (result.ToApplicationResult(), user.Id);

            if (roleId != null)
            {
                AppRole role = await _roleManager.FindByIdAsync(roleId);
                roleName = role.Name;
            }

            await _userManager.AddToRoleAsync(user, roleName);

            return (result.ToApplicationResult(), user.Id);
        }

        public async Task<AppRole> FindRoleByIdAsync(string roleId) =>
            await _roleManager.FindByIdAsync(roleId);

        public IQueryable<AppUser> FindByUserName(string username) =>
            _userManager.Users.Where(x => x.UserName.Equals(username));

        public async Task<AppUser> FindByUserNameAsync(string username) =>
            await _userManager.FindByNameAsync(username);

        public IQueryable<AppUser> FindUserById(string id) =>
             _userManager.Users.Where(x => x.Id.Equals(id));

        public IQueryable<AppRole> GetAllRoles() =>
            _roleManager.Roles;

        public IQueryable<AppUser> GetAllUsers() =>
            _userManager.Users;

        public async Task<string> GetRoleAsync(AppUser user)
        {
            IList<string> roles = await _userManager.GetRolesAsync(user);

            return roles == null ? null : roles.FirstOrDefault();
        }

        public async Task<bool> IsThereAnyRoleAsync() =>
            await _roleManager.Roles.AnyAsync();

        public async Task<bool> IsThereAnyUserAsync() =>
            await _userManager.Users.AnyAsync();

        public async Task<Result> UpdateUserAsync(AppUser user,
                                                  string firstName,
                                                  string lastName,
                                                  string userName,
                                                  string roleId)
        {
            user.FirstName = firstName;
            user.LastName = lastName;
            user.UserName = userName;

            IdentityResult result = await _userManager.UpdateAsync(user);

            await UpdateUserRoleAsync(user, roleId);

            return result.ToApplicationResult();
        }

        public async Task<AppUser> FindUserByIdAsync(string id) =>
            await _userManager.FindByIdAsync(id);

        private async Task UpdateUserRoleAsync(AppUser user, string roleId)
        {
            IList<string> userRoles = await _userManager.GetRolesAsync(user);
            AppRole currentRole = await _roleManager.FindByNameAsync(userRoles.FirstOrDefault());
            AppRole newRole = await _roleManager.FindByIdAsync(roleId);

            IdentityResult result = new IdentityResult();

            if (!roleId.Equals(currentRole.Id))
            {
                await _userManager.RemoveFromRolesAsync(user, userRoles);

                await _userManager.AddToRoleAsync(user, newRole.Name);
            }
        }

        public async Task DeleteUserAsync(AppUser user) =>
            await _userManager.DeleteAsync(user);
    }
}
