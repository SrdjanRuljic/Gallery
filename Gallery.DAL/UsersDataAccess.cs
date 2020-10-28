using Gallery.Common;
using Gallery.DAL.Interfaces;
using Gallery.DAL.Persistence;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Gallery.DAL
{
    public class UsersDataAccess : IUsersDataAccess
    {
        private readonly Connection _connection = new Connection();
        private readonly DbContext _dBContext = new DbContext();

        public async Task Delete(long id) =>
            await _dBContext.Delete("[dbo].[sp_Users.Delete]", id);

        public async Task<List<UserModel>> GetAll() =>
            await _dBContext.GetList<UserModel>("[dbo].[sp_Users.GetAll]");

        public async Task<UserModel> GetById(long id) =>
            await _dBContext.GetSingle<UserModel>("[dbo].[sp_Users.GetById]", id);

        public async Task<UserModel> GetByUsername(string username)
        {
            string queryString = "[dbo].[sp_Users.GetByUsername]";

            using (SqlConnection connection = new SqlConnection(_connection.ConnectionString()))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Username", username);

                UserModel model = new UserModel();

                try
                {
                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (!reader.HasRows)
                            model = null;

                        while (reader.Read())
                        {
                            model.Id = Convert.ToInt64(reader["Id"]);
                            model.FirstName = reader["FirstName"].ToString();
                            model.LastName = reader["LastName"].ToString();
                            model.Username = reader["Username"].ToString();
                            model.RoleId = Convert.ToInt64(reader["RoleId"]);
                            model.Role = reader["Role"].ToString();
                            model.PasswordHash = (byte[])reader["PasswordHash"];
                            model.PasswordSalt = (byte[])reader["PasswordSalt"];
                        }
                    }
                }
                catch (Exception exception)
                {
                    throw new Exception(exception.Message);
                }

                return model;
            }
        }

        public async Task<LogedInUserData> GetLogedInUserData(string username)
        {
            string queryString = "[dbo].[sp_Users.GetLogedInUserDataByUsername]";

            using (SqlConnection connection = new SqlConnection(_connection.ConnectionString()))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Username", username);

                LogedInUserData model = new LogedInUserData();

                try
                {
                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (!reader.HasRows)
                            model = null;

                        while (reader.Read())
                        {
                            model.DisplayName = reader["DisplayName"].ToString();
                            model.IsAdmin = Convert.ToBoolean(reader["IsAdmin"]);
                        }
                    }
                }
                catch (Exception exception)
                {
                    throw new Exception(exception.Message);
                }

                return model;
            }
        }

        public async Task<long> Insert(UserModel model) =>
            await _dBContext.Insert("[dbo].[sp_Users.Insert]", model);

        public async Task<bool> Update(UserModel model) =>
            await _dBContext.Update("[dbo].[sp_Users.Update]", model);

        public async Task<bool> UsernameExists(string username, long id)
        {
            string queryString = "[dbo].[sp_Users.UsernameExists]";

            using (SqlConnection connection = new SqlConnection(_connection.ConnectionString()))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Id", id);

                bool exists = false;

                try
                {
                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            exists = Convert.ToBoolean(reader["Result"]);
                        }
                    }
                }
                catch (Exception exception)
                {
                    throw new Exception(exception.Message);
                }

                return exists;
            }
        }
    }
}
