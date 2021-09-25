using Application.Common.Models;
using Domain.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IManagersServices
    {
        Task<AppUser> AuthenticateAsync(string username, string password);
        Task<Result> ChangePasswordAsync(AppUser user, string newPassword);
        Task CreateRoleAsync(string roleName);
        Task<(Result Result, string UserId)> CreateUserAsync(string firstName,
                                                             string lastName,
                                                             string userName,
                                                             string password,
                                                             string roleId = null,
                                                             string roleName = null);
        Task<AppUser> FindByUserNameAsync(string username);
        IQueryable<AppUser> FindByUserName(string username);
        Task<AppRole> FindRoleByIdAsync(string roleId);
        IQueryable<AppUser> FindUserById(string id);
        Task<AppUser> FindUserByIdAsync(string id);
        IQueryable<AppRole> GetAllRoles();
        IQueryable<AppUser> GetAllUsers();
        Task<string> GetRoleAsync(AppUser user);
        Task<bool> IsThereAnyUserAsync();
        Task<bool> IsThereAnyRoleAsync();
        Task DeleteUserAsync(AppUser user);
        Task<Result> UpdateUserAsync(AppUser user,
                                     string firstName,
                                     string lastName,
                                     string userName,
                                     string roleId);
    }
}
