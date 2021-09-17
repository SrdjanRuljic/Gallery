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

        public ManagersServices(UserManager<AppUser> userManager,
                                SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<AppUser> Authenticate(string username, string password)
        {
            AppUser user = new AppUser();

            SignInResult result = await _signInManager.PasswordSignInAsync(username, password, false, lockoutOnFailure: false);

            if (result.Succeeded)
                user = await _userManager.FindByNameAsync(username);

            return user;
        }

        public async Task<string> GetRole(AppUser user)
        {
            IList<string> roles = await _userManager.GetRolesAsync(user);

            return roles == null ? null : roles.FirstOrDefault();
        }
    }
}
