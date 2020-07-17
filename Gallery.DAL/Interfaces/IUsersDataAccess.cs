using Gallery.Common;
using System.Threading.Tasks;

namespace Gallery.DAL.Interfaces
{
    public interface IUsersDataAccess : IBaseDataAccess<UserModel>
    {
        Task<UserModel> GetByUsername(string username);
        Task<LogedInUserData> GetLogedInUserData(string username);
        Task<bool> UsernameExists(string username, long id);
    }
}
