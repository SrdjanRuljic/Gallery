using Application.Common.Interfaces;
using System.Threading.Tasks;

namespace Application.System.Commands.SeedData
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(IManagersServices _managersServicess)
        {
            //Seed Roles
            await _managersServicess.CreateRoleAsync(Domain.Roles.Admin.ToString());
            await _managersServicess.CreateRoleAsync(Domain.Roles.Moderator.ToString());
        }
    }
}
