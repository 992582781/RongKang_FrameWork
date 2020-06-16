using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Data;

namespace Redis
{
    /// <summary>
    /// Redis存储用户登录状态类，实现分布式登录
    /// </summary>
    public class RedisSession
    {
        private HttpContext context;
        Redis_Operate _Redis_Operate = new Redis_Operate();

        public RedisSession(bool IsReadOnly, int Timeout)
        {
            this.context = _Redis_Operate._Context();
            this.IsReadOnly = IsReadOnly;
            this.Timeout = Timeout;
            //更新缓存Key过期时间
            _Redis_Operate.KeyExpire(SessionID, DateTime.Now.AddMinutes(Timeout));
        }

        /// <summary>
        /// SessionId标识符
        /// </summary>
        public static string SessionName = "Redis_SessionId";

        //
        // 摘要:
        //     获取会话状态集合中的项数。
        //
        // 返回结果:
        //     集合中的项数。
        ////public int Count
        ////{
        ////    get
        ////    {
        ////        return RedisBase.Hash_GetCount(SessionID);
        ////    }
        ////}

        //
        // 摘要:
        //     获取一个值，该值指示会话是否为只读。
        //
        // 返回结果:
        //     如果会话为只读，则为 true；否则为 false。
        public bool IsReadOnly { get; set; }

        //
        // 摘要:
        //     获取会话的唯一标识符。
        //
        // 返回结果:
        //     唯一会话标识符。
        public string SessionID
        {
            get
            {
                return GetSessionID();
            }
        }

        //
        // 摘要:
        //     获取并设置在会话状态提供程序终止会话之前各请求之间所允许的时间（以分钟为单位）。
        //
        // 返回结果:
        //     超时期限（以分钟为单位）。
        public int Timeout { get; set; }

        /// <summary>
        /// 获取SessionID
        /// </summary>
        /// <param name="key">SessionId标识符</param>
        /// <returns>HttpCookie值</returns>
        private string GetSessionID()
        {
            HttpCookie cookie = context.Request.Cookies.Get(SessionName);
            if (cookie == null || string.IsNullOrEmpty(cookie.Value))
            {
                string newSessionID = Guid.NewGuid().ToString();
                HttpCookie newCookie = new HttpCookie(SessionName, newSessionID);
                newCookie.HttpOnly = IsReadOnly;
                newCookie.Expires = DateTime.Now.AddMinutes(Timeout);
                context.Response.Cookies.Add(newCookie);
                return "Session_" + newSessionID;
            }
            else
            {
                return "Session_" + cookie.Value;
            }
        }

        //
        // 摘要:
        //     按名称获取或设置会话值。
        //
        // 参数:
        //   name:
        //     会话值的键名。
        //
        // 返回结果:
        //     具有指定名称的会话状态值；如果该项不存在，则为 null。
        public object this[string name]
        {
            get
            {
                return _Redis_Operate.Hash_Get<object>(SessionID, name);
            }
            set
            {
                _Redis_Operate.Hash_Set<object>(SessionID, name, value);
            }
        }

        // 摘要:
        //     判断会话中是否存在指定key
        //
        // 参数:
        //   name:
        //     键值
        //
        public bool IsExistKey(string name)
        {
            return _Redis_Operate.Hash_Exist<object>(SessionID, name);
        }

        //
        // 摘要:
        //     向会话状态集合添加一个新项。
        //
        // 参数:
        //   name:
        //     要添加到会话状态集合的项的名称。
        //
        //   value:
        //     要添加到会话状态集合的项的值。
        public void Add(string name, object value)
        {
            _Redis_Operate.Hash_Set<object>(SessionID, name, value);
        }
        //
        // 摘要:
        //     从会话状态集合中移除所有的键和值。
        public void Clear()
        {
            _Redis_Operate.Hash_Remove(SessionID);
        }

        //
        // 摘要:
        //     删除会话状态集合中的项。
        //
        // 参数:
        //   name:
        //     要从会话状态集合中删除的项的名称。
        public void Remove(string name)
        {
            _Redis_Operate.Hash_Remove(SessionID, name);
        }
        //
        // 摘要:
        //     从会话状态集合中移除所有的键和值。
        public void RemoveAll()
        {
            Clear();
        }
    }
}