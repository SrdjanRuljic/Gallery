using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Application.System.Commands.SeedData
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new IdentityRole(Domain.Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Domain.Roles.Moderator.ToString()));
        }
    }
}
