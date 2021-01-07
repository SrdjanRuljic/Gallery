using Gallery.Common;
using Gallery.Common.UserModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gallery.DAL.Interfaces
{
    public interface IUsersDataAccess
    {
        Task Delete(long id);
        Task<List<ListUserModel>> GetAll();
        Task<UpdateUserModel> GetById(long id);
        Task<UserModel> GetByUsername(string username);
        Task<LogedInUserData> GetLogedInUserData(string username);
        Task<long> Insert(InsertUserModel model);
        Task<bool> UsernameExists(string username, long id);
        Task<bool> Update(UpdateUserModel model);
    }
}
