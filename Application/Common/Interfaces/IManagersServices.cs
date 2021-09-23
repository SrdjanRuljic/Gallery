using Application.Common.Models;
using Domain.Entities;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IManagersServices
    {
        Task<AppUser> AuthenticateAsync(string username, string password);
        Task CreateRoleAsync(string roleName);
        Task<(Result Result, string UserId)> CreateUserAsync(string firstName,
                                                             string lastName,
                                                             string userName,
                                                             string password,
                                                             string roleId = null,
                                                             string roleName = null);
        Task<AppUser> FindByUserNameAsync(string username);
        Task<AppRole> FindByIdAsync(string roleId);
        Task<string> GetRoleAsync(AppUser user);
        Task<bool> IsThereAnyUserAsync();
        Task<bool> IsThereAnyRoleAsync();
    }
}
