using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
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

        public async Task<IdentityResult> CreateAsync(string firstName, string lastName, string userName, string roleId)
        {
            AppUser user = new AppUser()
            {
                FirstName = firstName,
                LastName = lastName,
                UserName = userName
            };

            IdentityResult result = await _userManager.CreateAsync(user);

            if (!result.Succeeded)
                return result;

            AppRole role = await _roleManager.FindByIdAsync(roleId);

            await _userManager.AddToRoleAsync(user, role.Name);

            return result;
        }

        public async Task<AppRole> FindByIdAsync(string roleId) =>
            await _roleManager.FindByIdAsync(roleId);

        public async Task<AppUser> FindByUserNameAsync(string username) =>
            await _userManager.FindByNameAsync(username);

        public async Task<string> GetRoleAsync(AppUser user)
        {
            IList<string> roles = await _userManager.GetRolesAsync(user);

            return roles == null ? null : roles.FirstOrDefault();
        }
    }
}
