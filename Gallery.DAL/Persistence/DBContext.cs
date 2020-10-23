using Gallery.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Gallery.DAL.Persistence
{
    public partial class DbContext
    {
        private readonly Connection _connection = new Connection();

        public async Task Delete(string storeProcedureName, long id)
        {
            string queryString = storeProcedureName;

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

        public async Task<bool> Exists(string storeProcedureName, string name, long id)
        {
            string queryString = storeProcedureName;

            using (SqlConnection connection = new SqlConnection(_connection.ConnectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Name", name);
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

        public async Task<List<DropdownItemModel>> GetDropdownItems(string storeProcedureName)
        {
            string queryString = storeProcedureName;

            using (SqlConnection connection = new SqlConnection(_connection.ConnectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.CommandType = System.Data.CommandType.StoredProcedure;

                List<DropdownItemModel> list = new List<DropdownItemModel>();

                try
                {
                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            DropdownItemModel model = new DropdownItemModel()
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = reader["Name"].ToString()
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

        public async Task<long> Insert<T>(string storeProcedureName, T model)
        {
            string queryString = storeProcedureName;

            using (SqlConnection connection = new SqlConnection(_connection.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    DbParameterHelper.AddParameterFromInsertModel(command.Parameters, model);

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

        public async Task<bool> Update<T>(string storeProcedureName, T model)
        {
            string queryString = storeProcedureName;

            var isUpdated = false;

            using (SqlConnection connection = new SqlConnection(_connection.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    DbParameterHelper.AddParameterFromUpdateModel(command.Parameters, model);

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
    }
}
