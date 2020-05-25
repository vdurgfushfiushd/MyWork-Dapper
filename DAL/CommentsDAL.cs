using IDAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CommentsDAL: ICommentsDAL
    {
        MongoDBHelper<Comments> mHelp = new MongoDBHelper<Comments>("mongodb://127.0.0.1:27017", "fj", "Comments");

        /// <summary>
        /// 单个评论新增
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task AddAsync(Comments t)
        {
            await mHelp.AddAsync(t);
        }

        /// <summary>
        /// 多个评论新增
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public Task AddRangeAsync(IEnumerable<Comments> list)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 单个评论删除(软删除)
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Comments t)
        {
            await mHelp.DeleteAsync(t);
        }

        /// <summary>
        /// 动态条件删除(软删除)
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Expression<Func<Comments, bool>> exp)
        {
            await mHelp.DeleteAsync(exp);
        }

        /// <summary>
        /// 单个评论删除(真实删除)
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task Delete_trueAsync(Comments obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 批量删除(真实删除)
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public async Task Delete_trueAsync(IEnumerable<Comments> list)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 批量删除评论集合
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public async Task DeleteRangeAsync(IEnumerable<Comments> list)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Comments>> GetEntitiesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Comments> GetEntityAsync(Expression<Func<Comments, bool>> exp)
        {
            return await mHelp.GetEntityAsync(exp);
        }

        public async Task<Comments> GetEntityAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Comments>> GetFiltersAsync(Expression<Func<Comments, bool>> exp)
        {
            return await mHelp.GetFilterAsync(exp);
        }

        public async Task UpdateAsync(Comments t)
        {
            await mHelp.UpdateAsync(t);
        }

        public async Task AddRangeAsync(List<Comments> list)
        {
            throw new NotImplementedException();
        }

        public async Task Delete_trueAsync(Expression<Func<Comments, bool>> exp)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Comments>> GetFilterAsync(Expression<Func<Comments, bool>> exp)
        {
            throw new NotImplementedException();
        }
    }
}
