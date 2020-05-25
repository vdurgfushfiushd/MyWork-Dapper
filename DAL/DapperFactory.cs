using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DapperFactory
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        private static readonly string connstr = ConfigurationManager.ConnectionStrings["connstr"].ConnectionString;

        /// 返回连接实例        
        private static IDbConnection conn = null;

        /// <summary>
        /// 标识--用于确保线程同步
        /// </summary>
        private static readonly object locker = new object();

        /// <summary>
        /// 静态变量保存的实例
        /// </summary>
        private static DapperFactory dapperFactory;

        public static DapperFactory GetInstance()
        {
            // 双重锁定实现单例模式，在外层加个判空条件主要是为了减少加锁、释放锁的不必要的损耗
            if (dapperFactory == null)
            {
                lock (locker)
                {
                    if (dapperFactory == null)
                    {
                        dapperFactory = new DapperFactory();
                    }
                }
            }
            return dapperFactory;
        }

        /// <summary>
        /// 连接上下文
        /// </summary>
        public static IDbConnection GetDbConnection()
        {
            if (conn == null)
            {
                conn = new MySqlConnection(connstr);
            }
            //判断连接状态
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            return conn;
        }

        /// <summary>
        /// 事务开启
        /// </summary>
        /// <returns></returns>
        public static IDbTransaction GetDbTransaction() 
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            return conn.BeginTransaction();
        }
    }
}
