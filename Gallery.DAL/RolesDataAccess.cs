using Gallery.Common;
using Gallery.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Gallery.DAL
{
    public class RolesDataAccess : IRolesDataAccess
    {
        private readonly Connection _connection = new Connection();

        public async Task<List<DropdownItemModel>> GetDropdownItems()
        {
            string queryString = "[dbo].[sp_Roles.GetDropdownItems]";

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
    }
}
