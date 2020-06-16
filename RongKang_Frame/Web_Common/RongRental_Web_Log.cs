using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Web_Common
{
    public class RongRental_Web_Log
    {

        private static readonly object obj = new object();
        /// <summary>
        /// 写入错误日志
        /// </summary>
        /// <param name="logText">日志内容</param>
        /// <param name="mapPath">日志物理主要路径</param>
        public static void WriteLog(string logText, string mapPath="")
        {
            lock (obj)
            {
                if (string.IsNullOrEmpty(mapPath))
                {
                    mapPath = HttpContext.Current.Server.MapPath("~/Log/Error/");
                }

                StreamWriter writer = null;
                try
                {
                    //写入日志 
                    string year = DateTime.Now.Year.ToString();
                    string month = DateTime.Now.Month.ToString();
                    string day = DateTime.Now.Day.ToString();
                    string path = string.Empty;

                    //得到文件夹完全路径
                    string pathMonth = mapPath + year + "-" + month;

                    //判断文件是否存在
                    if (!Directory.Exists(pathMonth))//判断文件夹是否存在
                    {
                        //创建月份文件夹
                        Directory.CreateDirectory(pathMonth);
                    }

                    //得到日志文件的名称
                    string filename = DateTime.Now.ToString("yyyy-MM-dd") + ".log";

                    //得到日志文件的完整路径
                    path = pathMonth + "/" + filename;

                    FileInfo file = new FileInfo(path);
                    writer = new StreamWriter(file.FullName, true);//文件不在则创建，true表示追加
                    writer.WriteLine(logText);
                }
                finally
                {
                    if (writer != null)
                    {
                        writer.Close();
                    }
                }
            }
        }

 
        /// <summary>
        /// 写入添加修改数据的日志记录
        /// </summary>
        /// <param name="logText">日志内容</param>
        /// <param name="mapPath">日志物理主要路径</param>
        public static void WriteData(string logText, string mapPath = "")
        {
            lock (obj)
            {
                if (string.IsNullOrEmpty(mapPath))
                {
                    mapPath = HttpContext.Current.Server.MapPath("~/Log/Data/");
                }

                StreamWriter writer = null;
                try
                {
                    //写入日志 
                    string year = DateTime.Now.Year.ToString();
                    string month = DateTime.Now.Month.ToString();
                    string day = DateTime.Now.Day.ToString();
                    string path = string.Empty;

                    //得到文件夹完全路径
                    string pathMonth = mapPath + year + "-" + month;

                    //判断文件是否存在
                    if (!Directory.Exists(pathMonth))//判断文件夹是否存在
                    {
                        //创建月份文件夹
                        Directory.CreateDirectory(pathMonth);
                    }

                    //得到日志文件的名称
                    string filename = DateTime.Now.ToString("yyyy-MM-dd") + ".log";

                    //得到日志文件的完整路径
                    path = pathMonth + "/" + filename;

                    FileInfo file = new FileInfo(path);
                    writer = new StreamWriter(file.FullName, true);//文件不在则创建，true表示追加
                    writer.WriteLine(logText);
                }
                finally
                {
                    if (writer != null)
                    {
                        writer.Close();
                    }
                }
            }
        }

        public static string SetPicture(string path)
        {
            //写入日志 
            string year = DateTime.Now.Year.ToString();
            string month = DateTime.Now.Month.ToString();
            string day = DateTime.Now.Day.ToString();

            //得到文件夹完全路径
            string pathMonth = year + "-" + month;

            //判断文件是否存在
            if (!Directory.Exists(path + pathMonth))//判断文件夹是否存在
            {
                //创建月份文件夹
                Directory.CreateDirectory(path + pathMonth);

            }
            return pathMonth;
        }
    }
}