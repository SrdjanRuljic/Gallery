using Gallery.Common;
using Gallery.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Gallery.DAL
{
    public class PicturesDataAcces : IPicturesDataAccess
    {
        private readonly Connection _connection = new Connection();

        public async Task Delete(long id)
        {
            string queryString = "[dbo].[sp_Pictures.Delete]";

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

        public Task<List<PictureModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<PictureModel> GetById(long id)
        {
            string queryString = "[dbo].[sp_Pictures.GetById]";

            using (SqlConnection connection = new SqlConnection(_connection.ConnectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);

                PictureModel model = new PictureModel();

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
                            model.Name = reader["Name"].ToString();
                            model.CategoryId = Convert.ToInt32(reader["CategoryId"]);
                            model.Description = reader["Description"].ToString();
                            model.ImageName =  (Guid)(reader["ImageName"]);
                            model.Extension = reader["Extension"].ToString();
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

        public async Task<long> Insert(PictureModel model)
        {
            string queryString = "[dbo].[sp_Pictures.Insert]";

            using (SqlConnection connection = new SqlConnection(_connection.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Name", model.Name);
                    command.Parameters.AddWithValue("@CategoryId", model.CategoryId);
                    command.Parameters.AddWithValue("@Description", model.Description);
                    command.Parameters.AddWithValue("@ImageName", model.ImageName);
                    command.Parameters.AddWithValue("@Extension", model.Extension);
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

        public async Task<List<PictureModel>> Search()
        {
            string queryString = "[dbo].[sp_Pictures.Search]";

            using (SqlConnection connection = new SqlConnection(_connection.ConnectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.CommandType = System.Data.CommandType.StoredProcedure;

                List<PictureModel> list = new List<PictureModel>();

                try
                {
                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            PictureModel model = new PictureModel()
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = reader["Name"].ToString(),
                                CategoryId = Convert.ToInt32(reader["CategoryId"]),
                                Description = reader["Description"].ToString(),
                                ImageName = (Guid)reader["ImageName"],
                                Extension = reader["Extension"].ToString()
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

        public async Task<bool> Update(PictureModel model)
        {
            string queryString = "[dbo].[sp_Pictures.Update]";

            bool isUpdated = false;

            using (SqlConnection connection = new SqlConnection(_connection.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = model.Name;
                    command.Parameters.Add("@CategoryId", SqlDbType.BigInt).Value = model.CategoryId;
                    command.Parameters.Add("@Description", SqlDbType.NVarChar).Value = model.Description;
                    command.Parameters.Add("@ImageName", SqlDbType.UniqueIdentifier).Value = model.ImageName;
                    command.Parameters.Add("@Extension", SqlDbType.NVarChar).Value = model.Extension;
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
    }
}
