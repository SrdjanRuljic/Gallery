using System;
using System.Data.SqlClient;
using System.Reflection;

namespace Gallery.DAL.Persistence
{
    public static class DbParameterHelper
    {
        public static void AddParameterFromInsertModel<T>(SqlParameterCollection collection, T model)
        {
            foreach (PropertyInfo property in typeof(T).GetProperties())
            {
                if (property.Name == "Id")
                    collection.AddWithValue("@Id", 0);
                else if (property.Name == "Password")
                    return;
                else
                    collection.AddWithValue(String.Format("@{0}", property.Name),
                                            model?.GetType().GetProperty(property.Name)?.GetValue(model, null));
            }
        }

        public static void AddParameterFromUpdateModel<T>(SqlParameterCollection collection, T model)
        {
            foreach (PropertyInfo property in typeof(T).GetProperties())
            {
                collection.AddWithValue(String.Format("@{0}", property.Name),
                                        model?.GetType().GetProperty(property.Name)?.GetValue(model, null));
            }
        }

        public static void AddOnlyPrimaryKeyAsParametar(SqlParameterCollection collection, long id) =>
            collection.AddWithValue("@Id", id);

        public static void InitializeFromReader<T>(SqlDataReader reader, T model)
        {
            foreach (PropertyInfo property in typeof(T).GetProperties())
            {
                if (!ConversionHelper.ColumnExists(reader, property.Name))
                    continue;

                if (property.PropertyType == typeof(long?))
                    property.SetValue(model, ConversionHelper.ToInt64(reader[property.Name]));

                else if (property.PropertyType == typeof(int?))
                    property.SetValue(model, ConversionHelper.ToInt32(reader[property.Name]));

                else if (property.PropertyType == typeof(float?))
                    property.SetValue(model, ConversionHelper.ToFloat(reader[property.Name]));

                else if (property.PropertyType == typeof(string))
                    property.SetValue(model, ConversionHelper.ToString(reader[property.Name]));

                else if (property.PropertyType == typeof(bool?))
                    property.SetValue(model, ConversionHelper.ToBoolean(reader[property.Name]));

                else if (property.PropertyType == typeof(Guid?))
                    property.SetValue(model, ConversionHelper.ToGuid(reader[property.Name]));

                else if (property.PropertyType == typeof(DateTime?))
                    property.SetValue(model, ConversionHelper.ToDateTime(reader[property.Name]));

                else if (property.PropertyType == typeof(long))
                    property.SetValue(model, Convert.ToInt64(reader[property.Name]));

                else if (property.PropertyType == typeof(int))
                    property.SetValue(model, Convert.ToInt32(reader[property.Name]));

                else if (property.PropertyType == typeof(float))
                    property.SetValue(model, (float)Convert.ToDecimal(reader[property.Name]));

                else if (property.PropertyType == typeof(bool))
                    property.SetValue(model, Convert.ToBoolean(reader[property.Name]));

                else if (property.PropertyType == typeof(Guid))
                    property.SetValue(model, Guid.Parse(reader[property.Name].ToString()));

                else if (property.PropertyType == typeof(DateTime))
                    property.SetValue(model, DateTime.Parse(reader[property.Name].ToString()));
            }
        }
    }
}
