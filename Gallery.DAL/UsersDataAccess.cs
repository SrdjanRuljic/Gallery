using Gallery.Common;
using Gallery.DAL.Interfaces;
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

        public async Task Delete(long id)
        {
            string queryString = "[dbo].[sp_Users.Delete]";

            using (SqlConnection connection = new SqlConnection(_connection.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Id", SqlDbType.BigInt).Value = id;

                    try
                    {
                        await connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();
                    }
                    catch (Exception exception)
                    {
                        throw new Exception(exception.Message);
                    }
                }
            }
        }

        public async Task<List<UserModel>> GetAll()
        {
            string queryString = "[dbo].[sp_Users.GetAll]";

            using (SqlConnection connection = new SqlConnection(_connection.ConnectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.CommandType = System.Data.CommandType.StoredProcedure;

                List<UserModel> list = new List<UserModel>();

                try
                {
                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (!reader.HasRows)
                            list = null;

                        while (reader.Read())
                        {
                            UserModel model = new UserModel()
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Role = reader["Role"].ToString()
                            };
                            list.Add(model);
                        }
                    }
                }
                catch (Exception exception)
                {
                    throw new Exception(exception.Message);
                }

                return list;
            }
        }

        public async Task<UserModel> GetById(long id)
        {
            string queryString = "[dbo].[sp_Users.GetById]";

            using (SqlConnection connection = new SqlConnection(_connection.ConnectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);

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
                            model.Id = Convert.ToInt32(reader["Id"]);
                            model.FirstName = reader["FirstName"].ToString();
                            model.LastName = reader["LastName"].ToString();
                            model.Username = reader["Username"].ToString();
                            model.RoleId = Convert.ToInt32(reader["RoleId"]);
                            model.Role = reader["Name"].ToString();
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

        public async Task<UserModel> GetByUsername(string username)
        {
            string queryString = "[dbo].[sp_Users.GetByUsername]";

            using (SqlConnection connection = new SqlConnection(_connection.ConnectionString))
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

            using (SqlConnection connection = new SqlConnection(_connection.ConnectionString))
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

        public async Task<long> Insert(UserModel model)
        {
            string queryString = "[dbo].[sp_Users.Insert]";

            using (SqlConnection connection = new SqlConnection(_connection.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FirstName", model.FirstName);
                    command.Parameters.AddWithValue("@LastName", model.LastName);
                    command.Parameters.AddWithValue("@Username", model.Username);
                    command.Parameters.AddWithValue("@PasswordHash", model.PasswordHash);
                    command.Parameters.AddWithValue("@PasswordSalt", model.PasswordSalt);
                    command.Parameters.AddWithValue("@RoleId", model.RoleId);
                    command.Parameters.AddWithValue("@Id", 0);

                    long id = 0;

                    try
                    {
                        await connection.OpenAsync();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                id = Convert.ToInt32(reader["Id"]);
                            }
                        }
                    }
                    catch (Exception exception)
                    {
                        throw new Exception(exception.Message);
                    }

                    return id;
                }
            }
        }

        public async Task<bool> Update(UserModel model)
        {
            string queryString = "[dbo].[sp_Users.Update]";

            bool isUpdated = false;

            using (SqlConnection connection = new SqlConnection(_connection.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = model.FirstName;
                    command.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = model.LastName;
                    command.Parameters.Add("@Username", SqlDbType.NVarChar).Value = model.Username;
                    command.Parameters.Add("@RoleId", SqlDbType.BigInt).Value = model.RoleId;
                    command.Parameters.Add("@Id", SqlDbType.BigInt).Value = model.Id;

                    try
                    {
                        await connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();

                        isUpdated = true;
                    }
                    catch (Exception exception)
                    {
                        throw new Exception(exception.Message);
                    }
                }
            }

            return isUpdated;
        }

        public async Task<bool> UsernameExists(string username, long id)
        {
            string queryString = "[dbo].[sp_Users.UsernameExists]";

            using (SqlConnection connection = new SqlConnection(_connection.ConnectionString))
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
