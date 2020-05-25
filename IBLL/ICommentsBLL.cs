using DTO;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    /// <summary>
    /// 评论对象的业务逻辑层类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICommentsBLL
    {
        /// <summary>
        /// 单个新增
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task AddAsync(CommentsDTO commentsDTO);

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
        Task DeleteAsync(CommentsDTO commentsDTO);

        /// <summary>
        /// 动态条件删除(软删除)
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        Task DeleteAsync(Expression<Func<Comments, bool>> exp);

        /// <summary>
        /// 单个删除(真实删除)
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task Delete_trueAsync(CommentsDTO commentsDTO);

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
        Task UpdateAsync(CommentsDTO commentsDTO);

        /// <summary>
        /// 动态条件单个查询
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        Task<CommentsDTO> GetEntityAsync(Expression<Func<Comments, bool>> exp);

        /// <summary>
        /// 动态条件多个查询
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        Task<List<CommentsDTO>> GetFilterAsync(Expression<Func<Comments, bool>> exp);
    }
}
