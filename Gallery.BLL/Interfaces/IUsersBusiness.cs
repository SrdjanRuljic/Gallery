using Gallery.Common;
using System.Threading.Tasks;

namespace Gallery.BLL.Interfaces
{
    public interface IUsersBusiness : IBaseBusiness<UserModel>
    {
        Task<UserModel> GetByUsername(string username);
        public bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt);
        Task<bool> UsernameExists(string username, long id);
        Task<LogedInUserData> GetLogedInUserData(string username);
    }
}
