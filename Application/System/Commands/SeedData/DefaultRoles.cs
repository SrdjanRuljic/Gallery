using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Application.System.Commands.SeedData
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(RoleManager<AppRole> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new AppRole()
            {
                Name = Domain.Roles.Admin.ToString()
            });

            await roleManager.CreateAsync(new AppRole()
            {
                Name = Domain.Roles.Moderator.ToString()
            });
        }
    }
}
