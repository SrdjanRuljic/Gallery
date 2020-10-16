using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;

namespace Gallery.DAL.Persistence
{
    public static class DBParameterHelper
    {
        public static void AddParameterFromModel<T>(SqlParameterCollection collection, T model)
        {
            foreach (PropertyInfo item in typeof(T).GetProperties())
            {
                if (item.Name == "Id")
                {
                    collection.AddWithValue("@Id", 0);
                }

                collection.AddWithValue(String.Format("@{0}", item.Name),
                                        model?.GetType().GetProperty(item.Name)?.GetValue(model, null));
            }
        }
    }
}
