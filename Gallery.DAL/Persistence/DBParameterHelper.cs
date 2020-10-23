using System;
using System.Data.SqlClient;
using System.Reflection;

namespace Gallery.DAL.Persistence
{
    public static class DbParameterHelper
    {
        public static void AddParameterFromInsertModel<T>(SqlParameterCollection collection, T model)
        {
            foreach (PropertyInfo item in typeof(T).GetProperties())
            {
                if (item.Name == "Id")
                    collection.AddWithValue("@Id", 0);
                else
                    collection.AddWithValue(String.Format("@{0}", item.Name),
                                            model?.GetType().GetProperty(item.Name)?.GetValue(model, null));
            }
        }

        public static void AddParameterFromUpdateModel<T>(SqlParameterCollection collection, T model)
        {
            foreach (PropertyInfo item in typeof(T).GetProperties())
            {
                collection.AddWithValue(String.Format("@{0}", item.Name),
                                        model?.GetType().GetProperty(item.Name)?.GetValue(model, null));
            }
        }

        public static void AddOnlyPrimaryKeyAsParametar(SqlParameterCollection collection, long id) =>
            collection.AddWithValue("@Id", id);
    }
}
