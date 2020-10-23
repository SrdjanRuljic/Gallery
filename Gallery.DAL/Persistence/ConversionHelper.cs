using System;
using System.Data;

namespace Gallery.DAL.Persistence
{
    internal class ConversionHelper
    {
        public static long? ToInt64(Object o)
        {
            if (o != DBNull.Value)
                return Convert.ToInt64(o);
            else
                return null;
        }

        public static int? ToInt32(Object o)
        {
            if (o != DBNull.Value)
                return Convert.ToInt32(o);
            else
                return null;
        }

        public static long? ToInt16(Object o)
        {
            if (o != DBNull.Value)
                return Convert.ToInt16(o);
            else
                return null;
        }

        public static float? ToFloat(Object o)
        {
            if (o != DBNull.Value)
                return (float)Convert.ToDecimal(o);
            else
                return null;
        }

        public static string ToString(Object o)
        {
            if (o != DBNull.Value)
                return o.ToString();
            else
                return null;
        }

        public static bool? ToBoolean(Object o)
        {
            if (o != DBNull.Value)
                return Convert.ToBoolean(o);
            else
                return null;
        }

        public static Guid? ToGuid(Object o)
        {
            if (o != DBNull.Value)
                return Guid.Parse(o.ToString());
            else
                return null;
        }

        public static bool ColumnExists(IDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
                if (reader.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                    return true;

            return false;
        }

        //internal static InstanceType? ToInstanceType(Object o)
        //{
        //    if (o != DBNull.Value)
        //    {
        //        return (InstanceType?)Convert.ToInt64(o);
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        public static DateTime? ToDateTime(Object o)
        {
            if (o != DBNull.Value)
                return DateTime.Parse(o.ToString());
            else
                return null;
        }
    }
}
