using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IManagersServices
    {
        Task<AppUser> AuthenticateAsync(string username, string password);
        Task<IdentityResult> CreateAsync(string firstName, string lastName, string userName, string roleId);
        Task<AppUser> FindByUserNameAsync(string username);
        Task<AppRole> FindByIdAsync(string roleId);
        Task<string> GetRoleAsync(AppUser user);
    }
}
