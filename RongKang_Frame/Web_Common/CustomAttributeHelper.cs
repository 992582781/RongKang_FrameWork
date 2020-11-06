using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Web_Common
{
    public static class CustomAttributeHelper
    {

        #region 带缓存的，没有使用
        /// <summary>
        /// Cache Data
        /// </summary>
        private static readonly Dictionary<string, string> Cache = new Dictionary<string, string>();

        /// <summary>
        /// 获取CustomAttribute Value
        /// </summary>
        /// <typeparam name="T">Attribute的子类型</typeparam>
        /// <param name="sourceType">头部标有CustomAttribute类的类型</param>
        /// <param name="attributeValueAction">取Attribute具体哪个属性值的匿名函数</param>
        /// <returns>返回Attribute的值，没有则返回null</returns>
        public static string GetCustomAttributeValue<T>(this Type sourceType, Func<T, string> attributeValueAction) where T : Attribute
        {
            return GetAttributeValue(sourceType, attributeValueAction, null);
        }

        /// <summary>
        /// 获取CustomAttribute的自定义属性值 Value
        /// </summary>
        /// <typeparam name="T">Attribute的子类型</typeparam>
        /// <param name="sourceType">头部标有CustomAttribute类的类型</param>
        /// <param name="attributeValueAction">取Attribute具体哪个属性值的匿名函数</param>
        /// <param name="name">field name或property name</param>
        /// <returns>返回Attribute的值，没有则返回null</returns>
        public static string GetCustomAttributeValue<T>(this Type sourceType, Func<T, string> attributeValueAction,
            string name) where T : Attribute
        {
            return GetAttributeValue(sourceType, attributeValueAction, name);
        }

        private static string GetAttributeValue<T>(Type sourceType, Func<T, string> attributeValueAction,
            string name) where T : Attribute
        {
            var key = BuildKey(sourceType, name);
            if (!Cache.ContainsKey(key))
            {
                CacheAttributeValue(sourceType, attributeValueAction, name);
            }

            return Cache[key];
        }

        /// <summary>
        /// 缓存Attribute Value
        /// </summary>
        private static void CacheAttributeValue<T>(Type type,
            Func<T, string> attributeValueAction, string name)
        {
            var key = BuildKey(type, name);

            var value = GetValue(type, attributeValueAction, name);

            lock (key + "_attributeValueLockKey")
            {
                if (!Cache.ContainsKey(key))
                {
                    Cache[key] = value;
                }
            }
        }

        private static string GetValue<T>(Type type,
            Func<T, string> attributeValueAction, string name)
        {
            object attribute = null;
            if (string.IsNullOrEmpty(name))
            {
                attribute =
                    type.GetCustomAttributes(typeof(T), false).FirstOrDefault();
            }
            else
            {
                var propertyInfo = type.GetProperty(name);

                //if (type.GetProperty(name).PropertyType.Name.ToString() != "String")
                //{
                //    return null;
                //}

                if (propertyInfo != null)
                {
                    attribute =
                        propertyInfo.GetCustomAttributes(typeof(T), false).FirstOrDefault();
                }

                var fieldInfo = type.GetField(name);
                if (fieldInfo != null)
                {
                    attribute = fieldInfo.GetCustomAttributes(typeof(T), false).FirstOrDefault();
                }
            }

            return attribute == null ? null : attributeValueAction((T)attribute);
        }

        /// <summary>
        /// 缓存Collection Name Key
        /// </summary>
        private static string BuildKey(Type type, string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return type.FullName;
            }

            return type.FullName + "." + name;
        }

        #endregion


        #region CacheHelper缓存，使用中

        /// <summary>
        /// 获取某一个字段的值，在页面循环输出页面时使用
        /// </summary>
        public static string GetpropertyValue<T>(T item, string Field)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            var Value = "";
            foreach (PropertyInfo propertyInfo in properties)
            {
                if (propertyInfo.Name.ToString() == Field)
                {

                    Value = string.IsNullOrEmpty(propertyInfo.GetValue(item, null) + "") ? "" : propertyInfo.GetValue(item, null).ToString();

                    if (propertyInfo.PropertyType.Name.ToString() == "Int32")
                    {

                        foreach (PropertyInfo propertyInfo1 in properties)
                        {
                            if (propertyInfo1.Name.ToString() == Field + "Str")
                            {
                                Value = string.IsNullOrEmpty(propertyInfo1.GetValue(item, null) + "") ? "" : propertyInfo1.GetValue(item, null).ToString();
                                break;
                            }
                        }
                    }
                }
            }

            return Value;
        }


        /// <summary>
        /// 判断字段是否string和时间类型，页面下拉搜索使用
        /// </summary>
        public static bool GetpropertyTypeBool<T>(string Field)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (PropertyInfo propertyInfo in properties)
            {
                if (propertyInfo.Name.ToString() == Field)
                {
                    if (propertyInfo.PropertyType.Name.ToString() == "String" || propertyInfo.PropertyType.Name.ToString() == "DateTime")
                    {
                        return true;
                    }
                }
            }
            return false;
        }


        /// <summary>
        /// 获取所有字段名称和字段特性，页面添加修改与列表头显示数据使用使用,标识所有大于0的
        /// </summary>
        public static Dictionary<string, FieldNameAttribute> GetpropertyView<T>()
        {
            var Key = FieldNameAttributeBuildKey<T>();
            var Value = CacheHelper.Get(Key);
            if (Value == null)
            {
                CacheFieldNameAttribute<T>(Key);
            }
            return (Dictionary<string, FieldNameAttribute>)CacheHelper.Get(Key);
        }

        /// <summary>
        /// 缓存FieldNameAttribute
        /// </summary>
        private static void CacheFieldNameAttribute<T>(string Key)
        {
            Dictionary<string, FieldNameAttribute> dic = new Dictionary<string, FieldNameAttribute>();
            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (PropertyInfo propertyInfo in properties)
            {
                object[] FieldName = propertyInfo.GetCustomAttributes(typeof(FieldNameAttribute), false);
                if (FieldName.Length != 0)
                {
                    if (((FieldNameAttribute)FieldName[0]).View_Flag > 0)
                    {
                        dic.Add(propertyInfo.Name.ToString(), ((FieldNameAttribute)FieldName[0]));
                    }
                }
            }
            CacheHelper.Permanent(Key, dic);
        }


        /// <summary>
        /// 缓存FieldNameAttribute  Key
        /// </summary>
        private static string FieldNameAttributeBuildKey<T>()
        {
            var key = typeof(T).Name;
            return key;
        }


        /// <summary>
        /// 验证的model，和提交验证人的ID，或者用户名
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static string ValidateString<T>(T item, string ID)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            string Value = "";
            Validate Validate;
            var Name = "";
            var Result = "";
            var logText = "";
            logText += "\r\n发生时间：" + DateTime.Now.ToString();
            logText += "\r\n操作的表：" + typeof(T).Name.ToString();
            logText += "\r\n客户端IP：" + PageValidate.GetHostAddress();
            logText += "\r\n操作的ID：" + ID;
            logText += "\r\n操作数据：";
            foreach (PropertyInfo propertyInfo in properties)
            {

                object[] FieldName = propertyInfo.GetCustomAttributes(typeof(FieldNameAttribute), false);

                if (FieldName.Length != 0)
                {
                    Validate = ((FieldNameAttribute)FieldName[0]).Validate_Type;
                    Name = ((FieldNameAttribute)FieldName[0]).View_Name;


                    Value = string.IsNullOrEmpty(propertyInfo.GetValue(item, null) + "") ? "" : propertyInfo.GetValue(item, null).ToString();
                    if (!PageValidate.Page_Validate(Validate, Value))
                    {
                        Result = Name + Validate.GetEnumText() + "！";
                        return Result;
                    }
                    else
                    {
                        logText += propertyInfo.Name.ToString() + ":" + Value + ",";

                    }
                }
            }

            RongRental_Web_Log.WriteData(logText);
            return Result;
        }

        #endregion
    }
}

