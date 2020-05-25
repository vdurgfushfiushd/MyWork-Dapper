using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    public interface ICommentsDAL
    {
        /// <summary>
        /// 单个新增
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task AddAsync(Comments comments);

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        Task AddRangeAsync(List<Comments> list);

        /// <summary>
        /// 单个删除(软删除)
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task DeleteAsync(Comments comments);

        /// <summary>
        /// 动态条件删除(软删除)
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        Task DeleteAsync(Expression<Func<Comments,bool>> exp);

        /// <summary>
        /// 单个删除(真实删除)
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task Delete_trueAsync(Comments comments);

        /// <summary>
        /// 动态条件删除(真实删除)
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        Task Delete_trueAsync(Expression<Func<Comments, bool>> exp);

        /// <summary>
        /// 单个修改
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task UpdateAsync(Comments comments);

        /// <summary>
        /// 动态条件多个查询
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        Task<List<Comments>> GetFilterAsync(Expression<Func<Comments, bool>> exp);

        /// <summary>
        /// 动态条件单个查询
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        Task<Comments> GetEntityAsync(Expression<Func<Comments, bool>> exp);
    }
}
