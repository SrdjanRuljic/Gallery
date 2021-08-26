using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace Application.System.Commands.SeedData.DefaultUsers
{
    public static class Admin
    {
        public static async Task SeedAsync(UserManager<AppUser> userManager)
        {
            //Seed Default User
            AppUser admin = new AppUser
            {
                UserName = "admin",
                Email = "admin@gmail.com",
                FirstName = "Admin",
                LastName = "Admin",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };

            if (userManager.Users.All(u => u.Id != admin.Id))
            {
                AppUser user = await userManager.FindByNameAsync(admin.UserName);
                if (user == null)
                {
                    await userManager.CreateAsync(admin, "Administrator_123!");
                    await userManager.AddToRoleAsync(admin, Domain.Roles.Admin.ToString());
                }

            }
        }
    }
}
