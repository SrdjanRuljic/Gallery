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
        private readonly DbContext _dBContext = new DbContext();

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

        public async Task<CategoryModel> GetById(long id) =>
            await _dBContext.GetSingle<CategoryModel>("[dbo].[sp_Categories.GetById]", id);

        public async Task<List<DropdownItemModel>> GetDropdownItems() =>
            await _dBContext.GetDropdownItems("[dbo].[sp_Categories.GetDropdownItems]");

        public async Task<long> Insert(CategoryModel model) =>
           await _dBContext.Insert("[dbo].[sp_Categories.Insert]", model);

        public async Task<bool> Update(CategoryModel model) =>
            await _dBContext.Update("[dbo].[sp_Categories.Update]", model);
    }
}
