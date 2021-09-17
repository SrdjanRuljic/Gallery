using Domain.Entities;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IManagersServices
    {
        Task<AppUser> Authenticate(string username, string password);
        Task<string> GetRole(AppUser user);
    }
}
