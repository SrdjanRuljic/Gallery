using Application.Common.Interfaces;
using Domain.Entities;
using System.Threading.Tasks;

namespace Application.System.Commands.SeedData.DefaultUsers
{
    public static class Moderator
    {
        public static async Task SeedAsync(IManagersServices managersServices)
        {
            //Seed Default User
            AppUser moderator = new AppUser
            {
                UserName = "moderator",
                FirstName = "Moderator",
                LastName = "Moderator",
            };

            AppUser user = await managersServices.FindByUserNameAsync(moderator.UserName);
            if (user == null)
            {
                await managersServices.CreateUserAsync(moderator.FirstName,
                                                       moderator.LastName,
                                                       moderator.UserName,
                                                       "Moderator_123!",
                                                       null,
                                                       Domain.Roles.Moderator.ToString());
            }
        }
    }
}
