using AutoMapper;
using DTO;
using IBLL;
using IDAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CommentsBLL: ICommentsBLL
    {
        public IUserDAL userDAL { get; set; }

        public IRoleDAL roleDAL { get; set; }

        public IModuleDAL moduleDAL { get; set; }

        public INoteDAL noteDAL { get; set; }

        public ICommentsDAL commentsDAL { get; set; }

        /// <summary>
        /// 单个评论新增
        /// </summary>
        /// <param name="commentsDTO"></param>
        /// <returns></returns>
        public async Task AddAsync(CommentsDTO commentsDTO)
        {
            //redis
        }

        /// <summary>
        /// 评论批量新增
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public async Task AddRangeAsync(List<Comments> list)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 单个评论删除(软删除)
        /// </summary>
        /// <param name="commentsDTO"></param>
        /// <returns></returns>
        public async Task DeleteAsync(CommentsDTO commentsDTO)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 动态条件删除(软删除)
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Expression<Func<Comments, bool>> exp)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 单个评论删除(真实删除)
        /// </summary>
        /// <param name="commentsDTO"></param>
        /// <returns></returns>
        public async Task Delete_trueAsync(CommentsDTO commentsDTO)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 动态条件删除(真实删除)
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public async Task Delete_trueAsync(Expression<Func<Comments, bool>> exp)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 动态条件获取评论集合
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public async Task<List<CommentsDTO>> GetFilterAsync(Expression<Func<Comments, bool>> exp)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 动态条件获取单个评论
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public async Task<CommentsDTO> GetEntityAsync(Expression<Func<Comments, bool>> exp)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 单个评论修改
        /// </summary>
        /// <param name="commentsDTO"></param>
        /// <returns></returns>
        public async Task UpdateAsync(CommentsDTO commentsDTO)
        {
            throw new NotImplementedException();
        }
    }
}
