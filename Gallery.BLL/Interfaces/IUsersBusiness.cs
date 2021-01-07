using Gallery.Common;
using Gallery.Common.UserModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gallery.BLL.Interfaces
{
    public interface IUsersBusiness
    {
        Task Delete(long id);
        Task<UpdateUserModel> GetById(long id);
        Task<List<ListUserModel>> GetAll();
        Task<UserModel> GetByUsername(string username);
        Task<long> Insert(InsertUserModel model);
        public bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt);
        Task<bool> UsernameExists(string username, long id);
        Task<LogedInUserData> GetLogedInUserData(string username);
        Task<bool> Update(UpdateUserModel model);
    }
}
