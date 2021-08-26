using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace Application.System.Commands.SeedData.DefaultUsers
{
    public static class Moderator
    {
        public static async Task SeedAsync(UserManager<AppUser> userManager)
        {
            //Seed Default User
            AppUser moderator = new AppUser
            {
                UserName = "moderator",
                Email = "moderator@gmail.com",
                FirstName = "Moderator",
                LastName = "Moderator",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };

            if (userManager.Users.All(u => u.Id != moderator.Id))
            {
                AppUser user = await userManager.FindByNameAsync(moderator.UserName);
                if (user == null)
                {
                    await userManager.CreateAsync(moderator, "Administrator_123!");
                    await userManager.AddToRoleAsync(moderator, Domain.Roles.Moderator.ToString());
                }

            }
        }
    }
}
