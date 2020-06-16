using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace Redis
{
    public class ChangeMaster
    {
        public static ConnectionMultiplexer mex;
        public static EndPoint[] endpoints;
        public static void ChangeMasterer(IDatabase database)
        {
            mex = database.Multiplexer;
            endpoints = mex.GetEndPoints();
            if (endpoints.Count() < 2)
            {
                return;
            }
            //多个endpoint 才切换主备服务器
            List<EndPoint> connectedPoints = new List<EndPoint>();
            List<EndPoint> disconnetedPoints = new List<EndPoint>();
            foreach (var item in endpoints)
            {
                //判断哪些服务器可以连接
                var server = mex.GetServer(item);
                if (server.IsConnected)
                {
                    connectedPoints.Add(item);
                }
                else
                {
                    disconnetedPoints.Add(item);
                }
            }
            var connectedPoint = connectedPoints.FirstOrDefault();
            if (connectedPoint == null)
            {
                throw new Exception("没有可用的redis服务器");
            }

            mex.GetServer(connectedPoint).MakeMaster(ReplicationChangeOptions.All);
            for (int i = 1; i < connectedPoints.Count; i++)
            {
                mex.GetServer(connectedPoints[i]).SlaveOf(connectedPoint, CommandFlags.FireAndForget);
            }
            foreach (var item in disconnetedPoints)
            {
                mex.GetServer(item).SlaveOf(connectedPoint, CommandFlags.FireAndForget);
            }
        }
    }
}
