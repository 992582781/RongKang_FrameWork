using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;
using Newtonsoft.Json;
using Common;
using System.Web;

namespace Redis
{

    /// <summary>
    /// Redis数据存储操作类
    /// </summary>
    public class Redis_Operate : IRedis_Operate
    {
        static IDatabase database;
        static ConnectionMultiplexer conn;
        int DEFAULT_TMEOUT = 600;//默认超时时间（单位秒）
        JsonSerializerSettings jsonConfig = new JsonSerializerSettings() { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore, NullValueHandling = NullValueHandling.Ignore };
        public Redis_Operate()
        {
            conn = Manager.Instance.GetManager();
            ChangeMaster.ChangeMasterer(conn.GetDatabase());
            database = ChangeMaster.mex.GetDatabase();
        }
 

        class CacheObject<T>
        {
            public int ExpireTime { get; set; }
            public bool ForceOutofDate { get; set; }
            public T Value { get; set; }
        }


        /// <summary>
        /// 连接超时设置
        /// </summary>
        public int TimeOut
        {
            get
            {
                return DEFAULT_TMEOUT;
            }
            set
            {
                DEFAULT_TMEOUT = value;
            }
        }

        public HttpContext _Context()
        {
            return HttpContext.Current;
        }

 

        public object Get(string key)
        {
            return Get<object>(key);
        }

        public T Get<T>(string key)
        {

            DateTime begin = DateTime.Now;
            var cacheValue = database.StringGet(key);
            DateTime endCache = DateTime.Now;
            var value = default(T);
            if (!cacheValue.IsNull)
            {
                var cacheObject = JsonConvert.DeserializeObject<CacheObject<T>>(cacheValue, jsonConfig);
                if (!cacheObject.ForceOutofDate)
                    database.KeyExpire(key, new TimeSpan(0, 0, cacheObject.ExpireTime));
                value = cacheObject.Value;
            }
            DateTime endJson = DateTime.Now;

            return value;

        }

        public void Insert(string key, object data)
        {
            var currentTime = DateTime.Now;
            var timeSpan = currentTime.AddSeconds(TimeOut) - currentTime;
            DateTime begin = DateTime.Now;
            var jsonData = GetJsonData(data, TimeOut, false);
            DateTime endJson = DateTime.Now;
            database.StringSet(key, jsonData);
            DateTime endCache = DateTime.Now;
        }

        public void Insert(string key, object data, int cacheTime)
        {
            var currentTime = DateTime.Now;
            var timeSpan = TimeSpan.FromSeconds(cacheTime);
            DateTime begin = DateTime.Now;
            var jsonData = GetJsonData(data, TimeOut, true);
            DateTime endJson = DateTime.Now;
            database.StringSet(key, jsonData, timeSpan);
            DateTime endCache = DateTime.Now;
        }
        public void Insert(string key, object data, DateTime cacheTime)
        {
            var currentTime = DateTime.Now;
            var timeSpan = cacheTime - DateTime.Now;
            DateTime begin = DateTime.Now;
            var jsonData = GetJsonData(data, TimeOut, true);
            DateTime endJson = DateTime.Now;
            database.StringSet(key, jsonData, timeSpan);
            DateTime endCache = DateTime.Now;
        }

        public void Insert<T>(string key, T data)
        {
            var currentTime = DateTime.Now;
            var timeSpan = currentTime.AddSeconds(TimeOut) - currentTime;
            DateTime begin = DateTime.Now;
            var jsonData = GetJsonData<T>(data, TimeOut, false);
            DateTime endJson = DateTime.Now;
            database.StringSet(key, jsonData);
            DateTime endCache = DateTime.Now;
        }

        public void Insert<T>(string key, T data, int cacheTime)
        {
            var currentTime = DateTime.Now;
            var timeSpan = TimeSpan.FromSeconds(cacheTime);
            DateTime begin = DateTime.Now;
            var jsonData = GetJsonData<T>(data, TimeOut, true);
            DateTime endJson = DateTime.Now;
            database.StringSet(key, jsonData, timeSpan);
            DateTime endCache = DateTime.Now;
        }

        public void Insert<T>(string key, T data, DateTime cacheTime)
        {
            var currentTime = DateTime.Now;
            var timeSpan = cacheTime - DateTime.Now;
            DateTime begin = DateTime.Now;
            var jsonData = GetJsonData<T>(data, TimeOut, true);
            DateTime endJson = DateTime.Now;
            database.StringSet(key, jsonData, timeSpan);
            DateTime endCache = DateTime.Now;
        }


        string GetJsonData(object data, int cacheTime, bool forceOutOfDate)
        {
            var cacheObject = new CacheObject<object>() { Value = data, ExpireTime = cacheTime, ForceOutofDate = forceOutOfDate };
            return JsonConvert.SerializeObject(cacheObject, jsonConfig);//序列化对象
        }

        string GetJsonData<T>(T data, int cacheTime, bool forceOutOfDate)
        {
            var cacheObject = new CacheObject<T>() { Value = data, ExpireTime = cacheTime, ForceOutofDate = forceOutOfDate };
            return JsonConvert.SerializeObject(cacheObject, jsonConfig);//序列化对象
        }

        public void Remove(string key)
        {
            database.KeyDelete(key, CommandFlags.HighPriority);
        }

        /// <summary>
        /// 判断key是否存在
        /// </summary>
        public bool Exists(string key)
        {
            return database.KeyExists(key);
        }

        #region -- Hash --
        /// <summary> 
        /// 判断某个数据是否已经被缓存 
        /// </summary> 
        /// <typeparam name="T"></typeparam> 
        /// <param name="key"></param> 
        /// <param name="dataKey"></param> 
        /// <returns></returns> 
        public bool Hash_Exist<T>(string key, string dataKey)
        {

            return database.HashExists(key, dataKey);
            
        }

        /// <summary> 
        /// 存储数据到hash表 
        /// </summary> 
        /// <typeparam name="T"></typeparam> 
        /// <param name="key"></param> 
        /// <param name="dataKey"></param> 
        /// <returns></returns> 
        public bool Hash_Set<T>(string key, string dataKey, T t)
        {
            var value = GetJsonData<T>(t, TimeOut, true);
            return database.HashSet(key, dataKey, value);

        }
        /// <summary> 
        /// 移除hash中的某值 
        /// </summary> 
        /// <typeparam name="T"></typeparam> 
        /// <param name="key"></param> 
        /// <param name="dataKey"></param> 
        /// <returns></returns> 
        public  bool Hash_Remove(string key, string dataKey)
        {
            return database.HashDelete(key, dataKey);
        }

        /// <summary> 
        /// 移除整个hash 
        /// </summary> 
        /// <typeparam name="T"></typeparam> 
        /// <param name="key"></param> 
        /// <param name="dataKey"></param> 
        /// <returns></returns> 
        public bool Hash_Remove(string key)
        {

            return database.KeyDelete(key);
            
        }
   
        /// <summary> 
        /// 从hash表获取数据 
        /// </summary> 
        /// <typeparam name="T"></typeparam> 
        /// <param name="key"></param> 
        /// <param name="dataKey"></param> 
        /// <returns></returns> 
        public T Hash_Get<T>(string key, string dataKey)
        {
            var cacheValue = database.HashGet(key, dataKey);
            var value = default(T);
            if (!cacheValue.IsNull)
            {
                var cacheObject = JsonConvert.DeserializeObject<CacheObject<T>>(cacheValue, jsonConfig);
                value = cacheObject.Value;
            }

            return value;
        }
        
        /// <summary> 
        /// 设置缓存过期 
        /// </summary> 
        /// <param name="key"></param> 
        /// <param name="datetime"></param> 
        public bool Hash_SetExpire(string key, DateTime datetime)
        {
            return database.KeyExpire(key, datetime);
        }
 
        #endregion



        /// <summary>
        /// 设置key有效时间
        /// </summary>
        public   bool KeyExpire(string key, DateTime datetime)
        {
            return database.KeyExpire(key,datetime);
        }

 

    }
}