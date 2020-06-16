using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace Redis
{
    public sealed class Manager
    {
        private Manager() { }

        public static readonly Manager Instance = new Manager();

        public ConnectionMultiplexer GetManager()
        {
            return ConnectionMultiplexer.Connect(option);
        }

        static ConfigurationOptions option = new ConfigurationOptions()
        {
            EndPoints =
                            {
                                { "127.0.0.1", 63791 },
                                { "127.0.0.1", 63793 },
                                { "127.0.0.1", 63792 }
                            },
            AllowAdmin = true,
        };


        //public static ConnectionMultiplexer Manager
        //{
        //    get
        //    {
        //        if (_redis == null)
        //        {
        //            lock (_locker)
        //            {
        //                if (_redis != null) return _redis;

        //                _redis = GetManager();
        //                return _redis;
        //            }
        //        }

        //        return _redis;
        //    }
        //}

        //public ConnectionMultiplexer GetManager1()
        //{

        //    ConfigurationOptions sentinelConfig = new ConfigurationOptions();
        //    sentinelConfig.ServiceName = "master1";
        //    sentinelConfig.EndPoints.Add("192.168.2.3", 26379);
        //    sentinelConfig.EndPoints.Add("192.168.2.3", 26380);
        //    sentinelConfig.TieBreaker = "";//这行在sentinel模式必须加上
        //    sentinelConfig.CommandMap = CommandMap.Sentinel;
        //    // Need Version 3.0 for the INFO command?
        //    sentinelConfig.DefaultVersion = new Version(3, 0);
        //    ConnectionMultiplexer conn = ConnectionMultiplexer.Connect(sentinelConfig);
        //}
    }
}