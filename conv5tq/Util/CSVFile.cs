using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Conv5tq.Attributes;

namespace Conv5tq.Util
{
    /// <summary>
    /// CSVファイルのユーティリティクラス
    /// </summary>
    public static class CSVFile
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TCSV"></typeparam>
        /// <param name="path"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static IEnumerable<TCSV> ReadAllLines<TCSV>(string path, Encoding encoding = null) where TCSV : new()
        {
            var attr = GetCSVModelAttribute<TCSV>();
            if (attr == null)
            {
                throw new NotSupportedException();
            }

            foreach (var line in File.ReadAllLines(path, encoding ?? Encoding.UTF8))
            {
                var values = line.Split(attr.Separator);
                yield return DeserializeCSVObject<TCSV>(values);
            }
        }

        public static void WriteAllLines<TCSV>(string path, string name, TCSV content, bool isAppend, Encoding encoding = null) where TCSV : new()
        {
            TCSV[] contents = new TCSV[] { content };
            WriteAllLines<TCSV>(path, name, contents, isAppend, encoding);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TCSV"></typeparam>
        /// <param name="path"></param>
        /// <param name="contents"></param>
        /// <param name="encoding"></param>
        public static void WriteAllLines<TCSV>(string path, string name, IEnumerable<TCSV> contents, bool isAppend, Encoding encoding = null) where TCSV : new()
        {
            var attr = GetCSVModelAttribute<TCSV>();
            if (attr == null)
            {
                throw new NotSupportedException();
            }

            var fileName = string.Format(@"{0}\{1}", path, string.IsNullOrEmpty(name) ? attr.FileName : name);
            var values = contents.Select(x => SerializeCSVObject(x));
            if (isAppend)
            {
                File.AppendAllLines(fileName, values, encoding ?? Encoding.UTF8);
            }
            else
            {
                File.WriteAllLines(fileName, values, encoding ?? Encoding.UTF8);
            }
        }

        private static CSVModelAttribute GetCSVModelAttribute<TCSV>()
        {
            Type type = typeof(TCSV);
            return type.GetCustomAttribute<CSVModelAttribute>();
        }

        /// <summary>
        /// modelからCSVのstringに変換します。
        /// </summary>
        /// <typeparam name="TCSV"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static string SerializeCSVObject<TCSV>(TCSV obj)
        {
            var attr = GetCSVModelAttribute<TCSV>();
            var properties = GetCSVColumnProperties<TCSV>();
            var values = properties.Select(p => string.Format(p.GetCustomAttribute<CSVColumnAttribute>().Format, GetValue(obj, p)));
            return string.Join(attr.Separator.ToString(), values);
        }

        /// <summary>
        /// プロパティの値を取得します。
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        private static object GetValue<TCSV>(TCSV obj, PropertyInfo propertyInfo)
        {
            return propertyInfo.GetValue(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TCSV"></typeparam>
        /// <param name="values"></param>
        /// <returns></returns>
        private static TCSV DeserializeCSVObject<TCSV>(string[] values) where TCSV : new()
        {
            var result = new TCSV();
            var properties = GetCSVColumnProperties<TCSV>();
            foreach (var p in properties)
            {
                var attr = p.GetCustomAttribute<CSVColumnAttribute>();
                if (typeof(List<string>).IsAssignableFrom(p.PropertyType))
                {
                    // List<string>の場合
                    var listValue = values.Where((v, i) => i >= attr.Order).ToList();
                    p.SetValue(result, listValue);
                }
                else
                {
                    var value = values[attr.Order];
                    switch (Type.GetTypeCode(p.PropertyType))
                    {
                        case TypeCode.String:
                            p.SetValue(result, value);
                            break;
                        case TypeCode.Int32:
                            p.SetValue(result, int.Parse(value));
                            break;
                    }
                }
            }

            return result;
        }

        private static IEnumerable<PropertyInfo> GetCSVColumnProperties<TCSV>()
        {
            Type type = typeof(TCSV);
            var properties = type.GetProperties().
                Where(p => p.GetCustomAttribute<CSVColumnAttribute>() != null).
                OrderBy(p => (p.GetCustomAttribute<CSVColumnAttribute>()).Order);
            return properties;
        }
    }
}
