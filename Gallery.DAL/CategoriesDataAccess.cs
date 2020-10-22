using Gallery.Common;
using Gallery.DAL.Interfaces;
using Gallery.DAL.Persistence;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Gallery.DAL
{
    public class CategoriesDataAccess : ICategoriesDataAccess
    {
        private readonly Connection _connection = new Connection();
        private readonly DBContext _dBContext = new DBContext();

        public async Task Delete(long id) =>
            await _dBContext.Delete("[dbo].[sp_Categories.Delete]", id);

        public async Task<bool> Exists(string name, long id) =>
            await _dBContext.Exists("[dbo].[sp_Categories.Exists]", name, id);

        public async Task<List<CategoryModel>> GetAll()
        {
            string queryString = "[dbo].[sp_Categories.GetAll]";

            using (SqlConnection connection = new SqlConnection(_connection.ConnectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.CommandType = System.Data.CommandType.StoredProcedure;

                List<CategoryModel> list = new List<CategoryModel>();

                try
                {
                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            CategoryModel model = new CategoryModel()
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

        public async Task<CategoryModel> GetById(long id)
        {
            string queryString = "[dbo].[sp_Categories.GetById]";

            using (SqlConnection connection = new SqlConnection(_connection.ConnectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);

                CategoryModel model = new CategoryModel();

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

        public async Task<List<DropdownItemModel>> GetDropdownItems()
        {
            string queryString = "[dbo].[sp_Categories.GetDropdownItems]";

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

        public async Task<long> Insert(CategoryModel model) =>
           await _dBContext.Insert("[dbo].[sp_Categories.Insert]", model);

        public async Task<bool> Update(CategoryModel model) =>
            await _dBContext.Update("[dbo].[sp_Categories.Update]", model);
    }
}
