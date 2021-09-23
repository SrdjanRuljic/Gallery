using Application.Common.Interfaces;
using Domain.Entities;
using System.Threading.Tasks;

namespace Application.System.Commands.SeedData.DefaultUsers
{
    public static class Admin
    {
        public static async Task SeedAsync(IManagersServices managersServices)
        {
            //Seed Default User
            AppUser admin = new AppUser
            {
                UserName = "admin",
                FirstName = "Admin",
                LastName = "Admin",
            };

            AppUser user = await managersServices.FindByUserNameAsync(admin.UserName);
            if (user == null)
            {
                await managersServices.CreateUserAsync(admin.FirstName,
                                                       admin.LastName,
                                                       admin.UserName,
                                                       "Administrator_123!",
                                                       null,
                                                       Domain.Roles.Admin.ToString());
            }
        }
    }
}
