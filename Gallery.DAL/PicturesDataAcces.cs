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
    public class PicturesDataAcces : IPicturesDataAccess
    {
        private readonly Connection _connection = new Connection();
        private readonly DbContext _dBContext = new DbContext();

        public async Task Delete(long id) =>
            await _dBContext.Delete("[dbo].[sp_Pictures.Delete]", id);

        public Task<List<PictureModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<PictureDetailsModel> GetSingleById(long id) =>
            await _dBContext.GetSingle<PictureDetailsModel>("[dbo].[sp_Pictures.GetSingleById]", id);

        public async Task<long> Insert(PictureModel model) =>
            await _dBContext.Insert("[dbo].[sp_Pictures.Insert]", model);

        public async Task<List<PictureModel>> Search(string name, long? categoryId)
        {
            string queryString = "[dbo].[sp_Pictures.Search]";

            using (SqlConnection connection = new SqlConnection(_connection.ConnectionString()))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = name;
                command.Parameters.Add("@CategoryId", SqlDbType.BigInt).Value = categoryId;

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
                                Content = reader["Content"].ToString(),
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

        public async Task<bool> Update(PictureModel model) =>
            await _dBContext.Update("[dbo].[sp_Pictures.Update]", model);

        public async Task<PictureModel> GetById(long id) =>
            await _dBContext.GetSingle<PictureModel>("[dbo].[sp_Pictures.GetById]", id);
    }
}
