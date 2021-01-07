using Gallery.BLL.Interfaces;
using Gallery.Common;
using Gallery.Common.Helpers;
using Gallery.Common.UserModels;
using Gallery.Common.Validations;
using Gallery.Common.Validations.UserModelsValidations;
using Gallery.DAL;
using Gallery.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Gallery.BLL
{
    public class UsersBusiness : IUsersBusiness
    {
        private readonly IUsersDataAccess _usersDataAccess;

        public UsersBusiness()
        {
            _usersDataAccess = new UsersDataAccess();
        }

        public async Task Delete(long id)
        {
            try
            {
                await _usersDataAccess.Delete(id);
            }
            catch (Exception)
            {
                throw new ApplicationException(ErrorMessages.CanNotDeleteUser);
            }
        }

        public async Task<List<ListUserModel>> GetAll() =>
            await _usersDataAccess.GetAll();

        public async Task<UpdateUserModel> GetById(long id)
        {
            if (id <= 0)
            {
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, ErrorMessages.IdCanNotBeLowerThanOne);
            }

            UpdateUserModel user = await _usersDataAccess.GetById(id);

            if (user == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, ErrorMessages.UserNotFound);
            }

            return user;
        }

        public async Task<UserModel> GetByUsername(string username) =>
            await _usersDataAccess.GetByUsername(username);

        public async Task<LogedInUserData> GetLogedInUserData(string username)
        {
            LogedInUserData user = await _usersDataAccess.GetLogedInUserData(username);

            if (user == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, ErrorMessages.DataNotFound);
            }

            return user;
        }

        public async Task<long> Insert(InsertUserModel model)
        {
            long id = 0;
            string errorMessage = null;
            byte[] passwordHash, passwordSalt;

            if (!model.IsValid(out errorMessage))
            {
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, errorMessage);
            }

            model.Username = model.Username.ToLower();

            if (String.IsNullOrEmpty(model.FirstName) || String.IsNullOrWhiteSpace(model.FirstName))
                model.FirstName = null;

            if (String.IsNullOrEmpty(model.LastName) || String.IsNullOrWhiteSpace(model.LastName))
                model.LastName = null;

            bool exists = await _usersDataAccess.UsernameExists(model.Username, id);

            if (!exists)
            {
                CreatePasswordHash(model.Password, out passwordHash, out passwordSalt);

                model.PasswordHash = passwordHash;
                model.PasswordSalt = passwordSalt;

                id = await _usersDataAccess.Insert(model);
            }
            else
            {
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, ErrorMessages.UserExists);
            }

            return id;
        }

        public async Task<bool> Update(UpdateUserModel model)
        {
            var isUpdated = false;
            string errorMessage = null;

            if (!model.IsValid(out errorMessage))
            {
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, errorMessage);
            }

            model.Username = model.Username.ToLower();

            if (String.IsNullOrEmpty(model.FirstName) || String.IsNullOrWhiteSpace(model.FirstName))
                model.FirstName = null;

            if (String.IsNullOrEmpty(model.LastName) || String.IsNullOrWhiteSpace(model.LastName))
                model.LastName = null;

            var exists = await _usersDataAccess.UsernameExists(model.Username, model.Id);

            if (!exists)
            {
                isUpdated = await _usersDataAccess.Update(model);
            }
            else
            {
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, ErrorMessages.UserExists);
            }

            return isUpdated;
        }

        public async Task<bool> UsernameExists(string username, long id) =>
            await _usersDataAccess.UsernameExists(username, id);

        public bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (HMACSHA512 hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
            }

            return true;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (HMACSHA512 hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
