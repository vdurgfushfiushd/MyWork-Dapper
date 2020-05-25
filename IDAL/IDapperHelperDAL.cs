using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    public interface IDapperHelperDAL<T> where T:class
    {
        /// 增删改操作(单个)
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        Task<int> ExecuteAsync(string sql, object t);

        /// <summary>
        /// 增删改操作(批量)
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task<int> ExecuteAsync(string sql, List<T> list);

        /// <summary>
        /// 根据条件查询符合的集合
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetFilterAsync(string sql, Object obj);

        /// <summary>
        /// 根据条件查询符合的单个
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task<T> GetEntityAsync(string sql, Object obj);

        /// <summary>
        /// 动态查询
        /// </summary>
        /// <param name="dict"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> ByWhere(string sql, Dictionary<string, object> dict);

        /// <summary>
        /// 事务提交
        /// </summary>
        void Commit();

        /// <summary>
        /// 事务回滚
        /// </summary>
        void Rollback();
    }
}
