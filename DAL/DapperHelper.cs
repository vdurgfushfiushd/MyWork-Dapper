using log4net;
using Model;
using System.Configuration;
using Dapper;
using MySql.Data.MySqlClient;
using System.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using IDAL;
using System;

namespace DAL
{
    public class DapperHelper<T>: IDapperHelperDAL<T> where T:class 
    {

        public IDbConnection conn
        {
            get
            {
                var dapperFactory = DapperFactory.GetInstance();
                return DapperFactory.GetDbConnection();
            }
        }

        public IDbTransaction tx
        {
            get
            {
                var dapperFactory = DapperFactory.GetInstance();
                return DapperFactory.GetDbTransaction();
            }
        }

        /// <summary>
        /// 增删改操作(单个)
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task<int> ExecuteAsync(string sql, object t)
        {
            return await conn.ExecuteAsync(sql, t);
        }

        /// <summary>
        /// (动态增删改)增删改的动态条件操作
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task<int> ExecuteAsync(string sql, Dictionary<string, object> dict)
        {
            var DynamicParameters = new DynamicParameters();
            foreach (var key in dict.Keys)
            {
                var value = dict[key];
                sql += "and " + key + "=@" + key;
                DynamicParameters.Add(("@" + key), value);
            }
            return await conn.ExecuteAsync(sql, DynamicParameters);
        }

        /// <summary>
        /// 增删改操作(批量)
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<int> ExecuteAsync(string sql, List<T> list)
        {
            return await conn.ExecuteAsync(sql, list);
        }

        /// <summary>
        /// 根据条件查询符合的集合
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetFilterAsync(string sql, object obj)
        {
            return await conn.QueryAsync<T>(sql, obj);
        }

        /// <summary>
        /// 根据条件查询符合的单个
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<T> GetEntityAsync(string sql, object obj)
        {
            return await conn.QuerySingleOrDefaultAsync<T>(sql, obj);
        }

        /// <summary>
        /// 动态查询
        /// </summary>
        /// <param name="dict"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> ByWhere(string sql,Dictionary<string, object> dict)
        {
            var DynamicParameters = new DynamicParameters();
            foreach (var key in dict.Keys)
            {
                var value = dict[key];
                sql += " and " + key + "=@" + key;
                DynamicParameters.Add(("@" + key), value);
            }
            return await conn.QueryAsync<T>(sql, DynamicParameters);
        }

        public void Commit()
        {
            tx.Commit();
        }

        public void Rollback()
        {
            tx.Rollback();
        }
    }
}
