using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace CodeTonightBlog.DAL.Common
{
    public static class TConverter
    {
        public static T ChangeType<T>(object value)
        {
            return (T)ChangeType(typeof(T), value);
        }
        public static object ChangeType(Type t, object value)
        {
            TypeConverter tc = TypeDescriptor.GetConverter(t);
            return tc.ConvertFrom(value);
        }

        public static DataTable ToDataTable<T>(this IEnumerable<T> list)
        {
            Type elementType = typeof(T);
            using (DataTable t = new DataTable())
            {
                PropertyInfo[] _props = elementType.GetProperties();
                foreach (PropertyInfo propInfo in _props)
                {
                    Type _pi = propInfo.PropertyType;
                    Type ColType = Nullable.GetUnderlyingType(_pi) ?? _pi;
                    t.Columns.Add(propInfo.Name, ColType);
                }
                foreach (T item in list)
                {
                    DataRow row = t.NewRow();
                    foreach (PropertyInfo propInfo in _props)
                    {
                        row[propInfo.Name] = propInfo.GetValue(item, null) ?? DBNull.Value;
                    }
                    t.Rows.Add(row);
                }
                return t;
            }
        }
    }
}