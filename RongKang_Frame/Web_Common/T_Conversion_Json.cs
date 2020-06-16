using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Web_Common
{
    public class T_Conversion_Json
    {
        public T_Conversion_Json()
        {

        }

        /// <summary>  
        /// List转成json   
        /// </summary>  
        /// <typeparam name="T"></typeparam>  
        /// <param name="jsonName"></param>  
        /// <param name="list"></param>  
        /// <returns></returns>  
        public static string ListToJson<T>(IList<T> list, string jsonName, int code, string msg)
        {
            StringBuilder Json = new StringBuilder();
            if (string.IsNullOrEmpty(jsonName))
                jsonName = list[0].GetType().Name;

            Json.Append("{");
            Json.Append("\"code\":");
            Json.Append(code);
            Json.Append(",");
            Json.Append("\"msg\":");
            Json.Append(StringFormat(msg, typeof(string)));
            Json.Append(",");

            Json.Append("\"result\":");


            Json.Append("{\"" + jsonName + "\":[");

            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    T obj = Activator.CreateInstance<T>();
                    PropertyInfo[] pi = obj.GetType().GetProperties();
                    Json.Append("{");
                    for (int j = 0; j < pi.Length; j++)
                    {
                        Type type;
                        object o = pi[j].GetValue(list[i], null);
                        string v = string.Empty;
                        if (o != null)
                        {
                            type = o.GetType();
                            v = o.ToString();
                        }
                        else
                        {
                            type = typeof(string);
                        }

                        Json.Append("\"" + pi[j].Name.ToString() + "\":" + StringFormat(v, type));

                        if (j < pi.Length - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < list.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }

            Json.Append("]}");




            Json.Append("}");

            return Json.ToString();
        }
        /// <summary>  
        /// List转成json   
        /// </summary>  
        /// <typeparam name="T"></typeparam>  
        /// <param name="list"></param>  
        /// <returns></returns>  
        public static string ListToJson<T>(IList<T> list, int code, string msg)
        {
            object obj = list[0];
            return ListToJson<T>(list, obj.GetType().Name, code, msg);
        }




        /// <summary>
        /// 泛型 结果 转换成 json 序列化转化  
        /// </summary>
        /// <typeparam name="T">转换的类型</typeparam>
        /// <param name="jsonResName"></param>
        /// <param name="obj">要转换的data </param>
        /// <returns></returns>
        public static string TSerializeToJson<T>(T obj, int code, string msg)
        {
            JavaScriptSerializer jsSer = new JavaScriptSerializer();
            string JsonResult = jsSer.Serialize(obj).Replace("null", "\"\"");
            string jsonResName = "ResultData";
            Type t = Type.GetType(System.Text.RegularExpressions.Regex.Match(obj.GetType().FullName.Replace("[[", "[").Replace("]]", "]"), @"\[(?<type>[^\]]+)\]").Groups["type"].Value);
            if (null == t)
                jsonResName = obj.GetType().Name;
            else
                jsonResName = t.Name;

            StringBuilder br = new StringBuilder();
            br.Append("{");
            br.Append("\"code\":");
            br.Append(code);
            br.Append(",");
            br.Append("\"msg\":");
            br.Append(StringFormat(msg, typeof(string)));
            br.Append(",");

            br.Append("\"result\":");
            br.Append("{\"" + jsonResName + "\":[");
            br.Append(JsonResult);
            br.Append("]}");
            br.Append("}");

            return br.ToString();


        }


        /// <summary>
        /// json 转换成 泛型结构
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonStr"></param>
        /// <returns></returns>
        public static T JsonDeserializeToT<T>(string jsonStr) where T : class
        {
            T t = Activator.CreateInstance<T>();
            JavaScriptSerializer jsSer = new JavaScriptSerializer();
            if (string.IsNullOrEmpty(jsonStr))
            {
                return t;
            }
            else
            {
                return (T)jsSer.Deserialize<T>(jsonStr);
            }

        }







        /// <summary>  
        /// List转成json   
        /// </summary>  
        /// <typeparam name="T"></typeparam>  
        /// <param name="list"></param>  
        /// <returns></returns>  
        public static string ModelToJson<T>(T model, int code, string msg)
        {
            List<T> list = new List<T>();
            list.Add(model);
            object obj = list[0];
            return ListToJson<T>(list, obj.GetType().Name, code, msg);
        }

        /// <summary>   
        /// 对象转换为Json字符串   
        /// </summary>   
        /// <param name="jsonObject">对象</param>   
        /// <returns>Json字符串</returns>   
        public static string ToJson(object jsonObject)
        {
            string jsonString = "{";
            PropertyInfo[] propertyInfo = jsonObject.GetType().GetProperties();
            for (int i = 0; i < propertyInfo.Length; i++)
            {
                object objectValue = propertyInfo[i].GetGetMethod().Invoke(jsonObject, null);
                string value = string.Empty;
                if (objectValue is DateTime || objectValue is Guid || objectValue is TimeSpan)
                {
                    value = "'" + objectValue.ToString() + "'";
                }
                else if (objectValue is string)
                {
                    value = "'" + ToJson(objectValue.ToString()) + "'";
                }
                else if (objectValue is IEnumerable)
                {
                    value = ToJson((IEnumerable)objectValue);
                }
                else
                {
                    value = ToJson(objectValue.ToString());
                }
                jsonString += "\"" + ToJson(propertyInfo[i].Name) + "\":" + value + ",";
            }
            jsonString.Remove(jsonString.Length - 1, jsonString.Length);
            return jsonString + "}";
        }

        /// <summary>   
        /// 对象集合转换Json   
        /// </summary>   
        /// <param name="array">集合对象</param>   
        /// <returns>Json字符串</returns>   
        public static string ToJson(IEnumerable array)
        {
            string jsonString = "[";
            foreach (object item in array)
            {
                jsonString += ToJson(item) + ",";
            }
            jsonString.Remove(jsonString.Length - 1, jsonString.Length);
            return jsonString + "]";
        }

        /// <summary>   
        /// 普通集合转换Json   
        /// </summary>   
        /// <param name="array">集合对象</param>   
        /// <returns>Json字符串</returns>   
        public static string ToArrayString(IEnumerable array)
        {
            string jsonString = "[";
            foreach (object item in array)
            {
                jsonString = ToJson(item.ToString()) + ",";
            }
            jsonString.Remove(jsonString.Length - 1, jsonString.Length);
            return jsonString + "]";
        }

        /// <summary>   
        /// Datatable转换为Json   
        /// </summary>   
        /// <param name="table">Datatable对象</param>   
        /// <returns>Json字符串</returns>   
        public static string ToJson(DataTable dt)
        {
            StringBuilder jsonString = new StringBuilder();
            jsonString.Append("[");
            DataRowCollection drc = dt.Rows;
            for (int i = 0; i < drc.Count; i++)
            {
                jsonString.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    string strKey = dt.Columns[j].ColumnName;
                    string strValue = drc[i][j].ToString();
                    Type type = dt.Columns[j].DataType;

                    jsonString.Append("\"" + strKey + "\":");
                    if (type.Name == "Int32")
                    {
                        //if (strValue.ToInt(0) == 0)
                        //{
                        //    strValue = "0";
                        //}
                        //else
                        //{
                        strValue = StringFormat(strValue, type);
                        //}
                    }
                    else
                    {
                        strValue = StringFormat(strValue, type);
                    }


                    if (j < dt.Columns.Count - 1)
                    {
                        jsonString.Append(strValue + ",");
                    }
                    else
                    {
                        jsonString.Append(strValue);
                    }
                }
                jsonString.Append("},");
            }
            jsonString.Remove(jsonString.Length - 1, 1);
            jsonString.Append("]");
            return jsonString.ToString();
        }

        /// <summary>  
        /// DataTable转成Json   
        /// </summary>  
        /// <param name="jsonName"></param>  
        /// <param name="dt"></param>  
        /// <returns></returns>  
        public static string ToJson(DataTable dt, string jsonName)
        {
            StringBuilder Json = new StringBuilder();
            if (string.IsNullOrEmpty(jsonName))
                jsonName = dt.TableName;
            Json.Append("{\"" + jsonName + "\":[");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Json.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        Type type = dt.Rows[i][j].GetType();
                        Json.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":" + StringFormat(dt.Rows[i][j].ToString(), type));
                        if (j < dt.Columns.Count - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < dt.Rows.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
            Json.Append("]}");
            return Json.ToString();
        }
        // <summary>   
        /// DataReader转换为Json   
        /// </summary>   
        /// <param name="dataReader">DataReader对象</param>   
        /// <returns>Json字符串</returns>   
        public static string ToJson(DbDataReader dataReader, string jsonName = "jsonName", int code = 1, string msg = "")
        {
            try
            {
                StringBuilder Json = new StringBuilder();
                Json.Append("{");
                Json.Append("\"code\":");
                Json.Append(code);
                Json.Append(",");
                Json.Append("\"msg\":");
                Json.Append(StringFormat(msg, typeof(string)));
                Json.Append(",");

                Json.Append("\"result\":");


                Json.Append("{\"" + jsonName + "\":[");

                while (dataReader.Read())
                {
                    Json.Append("{");
                    for (int i = 0; i < dataReader.FieldCount; i++)
                    {
                        Type type = dataReader.GetFieldType(i);
                        string strKey = dataReader.GetName(i);
                        string strValue = dataReader[i].ToString();
                        Json.Append("\"" + strKey + "\":");
                        strValue = StringFormat(strValue, type);
                        if (i < dataReader.FieldCount - 1)
                        {
                            Json.Append(strValue + ",");
                        }
                        else
                        {
                            Json.Append(strValue);
                        }
                    }
                    Json.Append("},");
                }
                if (!dataReader.IsClosed)
                {
                    dataReader.Close();
                }
                Json.Remove(Json.Length - 1, 1);

                Json.Append("]}");
                Json.Append("}");
                if (Json.Length == 1)
                {
                    return "[]";
                }
                return Json.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>   
        /// DataSet转换为Json   
        /// </summary>   
        /// <param name="dataSet">DataSet对象</param>   
        /// <returns>Json字符串</returns>   
        public static string ToJson(DataSet dataSet)
        {
            string jsonString = "{";
            foreach (DataTable table in dataSet.Tables)
            {
                jsonString += "\"" + table.TableName + "\":" + ToJson(table) + ",";
            }
            jsonString = jsonString.TrimEnd(',');
            return jsonString + "}";
        }



        /// <summary>   
        /// 输出一个没有列表的空json 因为当我们返回列表数据为空时，可以这样调用
        /// </summary>   
        /// <param name="array">集合对象</param>   
        /// <returns>Json字符串</returns>   
        public static string ToJson(int code, string msg)
        {
            try
            {
                StringBuilder Json = new StringBuilder();
                Json.Append("{");
                Json.Append("\"code\":");
                Json.Append(code);
                Json.Append(",");
                Json.Append("\"msg\":");
                Json.Append(StringFormat(msg, typeof(string)));
                Json.Append(",");
                Json.Append("\"result\":");
                Json.Append("{");
                Json.Append("}");
                Json.Append("}");

                return Json.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>  
        /// 过滤特殊字符  
        /// 
        /// </summary>  
        /// <param name="s"></param>  
        /// <returns></returns>  
        private static string String2Json(String s)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
                char c = s.ToCharArray()[i];
                switch (c)
                {
                    case '\"':
                        sb.Append("\\\""); break;
                    case '\\':
                        sb.Append("\\\\"); break;
                    case '/':
                        sb.Append("\\/"); break;
                    case '\b':
                        sb.Append("\\b"); break;
                    case '\f':
                        sb.Append("\\f"); break;
                    case '\n':
                        sb.Append("\\n"); break;
                    case '\r':
                        sb.Append("\\r"); break;
                    case '\t':
                        sb.Append("\\t"); break;
                    case '\v':
                        sb.Append("\\v"); break;
                    case '\0':
                        sb.Append("\\0"); break;
                    default:
                        sb.Append(c); break;
                }
            }
            return sb.ToString();
        }

        /// <summary>  
        /// 格式化字符型、日期型、布尔型  
        /// </summary>  
        /// <param name="str"></param>  
        /// <param name="type"></param>  
        /// <returns></returns>  
        private static string StringFormat(string str, Type type)
        {
            if (type != typeof(string) && string.IsNullOrEmpty(str))
            {
                str = "\"" + str + "\"";
            }
            else if (type == typeof(string))
            {
                str = String2Json(str);
                str = "\"" + str + "\"";
            }
            else if (type == typeof(DateTime))
            {
                str = "\"" + str + "\"";
            }
            else if (type == typeof(bool))
            {
                str = str.ToLower();
            }
            else if (type == typeof(byte[]))
            {
                str = "\"" + str + "\"";
            }
            else if (type == typeof(Guid))
            {
                str = "\"" + str + "\"";
            }
            return str;
        }



        /// <summary>
        /// JSON格式数组转化为对应的List<T>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="JsonStr">JSON格式数组</param>
        /// <returns></returns>
        public static List<T> JSONStringToList<T>(string JsonStr)
        {
            JavaScriptSerializer Serializer = new JavaScriptSerializer();
            //设置转化JSON格式时字段长度
            List<T> objs = Serializer.Deserialize<List<T>>(JsonStr);
            return objs;
        }

        /// <summary>
        ///  List<T>转JSONString
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="JsonStr">JSON格式数组</param>
        /// <returns></returns>
        public static string ListToJSONString<T>(IList<T> list)
        {
            JavaScriptSerializer Serializer = new JavaScriptSerializer();

            return Serializer.Serialize(list);
        }
    }
}
