using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    public interface IChatUserFriendDAL
    {
        /// <summary>
        /// 单个添加好友
        /// </summary>
        /// <param name="chatUserFriend"></param>
        /// <returns></returns>
        Task AddAsync(ChatUserFriend chatUserFriend);

        /// <summary>
        /// 批量添加好友
        /// </summary>
        /// <param name="chatUserFriend"></param>
        /// <returns></returns>
        Task AddAsync(List<ChatUserFriend> chatUserFriends);
          
        /// <summary>
        /// 单个删除好友
        /// </summary>
        /// <param name="chatUserFriend"></param>
        /// <returns></returns>
        Task DeleteAsync(ChatUserFriend chatUserFriend);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="chatUserFriends"></param>
        /// <returns></returns>
        Task DeleteAsync(List<ChatUserFriend> chatUserFriends);

        /// <summary>
        /// 动态查询单个好友对象
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        Task<ChatUserFriend> GetEntityAsync(Expression<Func<ChatUserFriend, bool>> exp);

        /// <summary>
        /// 动态查询多个好友对象
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        Task<IQueryable<ChatUserFriend>> GetFilterAsync(Expression<Func<ChatUserFriend, bool>> exp);
    }
}
