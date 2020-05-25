using DTO;
using Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IBLL
{
    /// <summary>
    /// 聊天用户对象的业务逻辑层类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IChatUserBLL
    {
        /// <summary>
        /// 用户新增
        /// </summary>
        /// <param name="chatUserDTO"></param>
        /// <returns></returns>
        Task AddAsync(ChatUserDTO chatUserDTO);

        /// <summary>
        /// 单个删除(软删除)
        /// </summary>
        /// <param name="chatUserDTO"></param>
        /// <returns></returns>
        Task DeleteAsync(ChatUserDTO chatUserDTO);

        /// <summary>
        /// 动态条件单个删除(软删除)
        /// </summary>
        /// <param name="chatUserDTO"></param>
        /// <returns></returns>
        Task DeleteAsync(Expression<Func<ChatUser, bool>> exp);

        /// <summary>
        /// 单个删除(真实删除)
        /// </summary>
        /// <param name="chatUserDTO"></param>
        /// <returns></returns>
        Task Delete_TrueAsync(ChatUserDTO chatUserDTO);

        /// <summary>
        /// 动态条件单个删除(真实删除)
        /// </summary>
        /// <param name="chatUserDTO"></param>
        /// <returns></returns>
        Task Delete_TrueAsync(Expression<Func<ChatUser, bool>> exp);

        /// <summary>
        /// 动态条件单个获取
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        Task<ChatUserDTO> GetEntityAsync(Expression<Func<ChatUser,bool>> exp);

        /// <summary>
        /// 动态条件多个获取
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        Task<List<ChatUserDTO>> GetFilterAsync(Expression<Func<ChatUser, bool>> exp);

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <param name="CurrentChatName"></param>
        /// <returns></returns>
        Task ExitAsync(Expression<Func<ChatUser, bool>> exp);

        /// <summary>
        /// 向指定用户发送消息
        /// </summary>
        /// <param name="messageDTO"></param>
        /// <returns></returns>
        Task SendMessageAsync(MessageDTO messageDTO);

        /// <summary>
        /// 获取指定用户到接收者之间的聊天消息
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="Receiver"></param>
        /// <returns></returns>
        Task<List<MessageDTO>> GetSendMessageAsync(string Sender, string Receiver);

        /// <summary>
        /// 创建群组
        /// </summary>
        /// <param name="groupDTO"></param>
        Task CreateGroupAsync(GroupDTO groupDTO);

        /// <summary>
        /// 对象所属的群组的集合
        /// </summary>
        /// <param name="ChatName"></param>
        /// <returns></returns>
        Task<List<GroupDTO>> GetGroupAsync(string ChatName);

        /// <summary>
        /// 单个修改
        /// </summary>
        /// <param name="chatUserDTO"></param>
        /// <returns></returns>
        Task UpdateAsync(ChatUserDTO chatUserDTO,string[] Group_Ids);

        /// <summary>
        /// 获取要修改的chatuser
        /// </summary>
        /// <param name="Id">聊天用户的id</param>
        /// <returns></returns>
        Task<ChatUserGroupDTO> GetUpdateChatUserAsync(string Id);

        /// <summary>
        /// 聊天用户登录
        /// </summary>
        /// <param name="chatUserDTO"></param>
        /// <returns></returns>
        Task<ChatUserDTO> ChatUserLogin(ChatUserDTO chatUserDTO);

        /// <summary>
        /// 聊天用户加入指定的群组
        /// </summary>
        /// <param name="chatUserDTO"></param>
        /// <param name="groupDTO"></param>
        /// <returns></returns>
        Task JoinGroupAsync(ChatUserDTO chatUserDTO, GroupDTO groupDTO);

        /// <summary>
        /// 单个添加好友
        /// </summary>
        /// <param name="chatUserFriendDTO">添加人的id</param>
        /// <returns></returns>
        Task AddFriendAsync(ChatUserFriendDTO chatUserFriendDTO);

        /// <summary>
        /// 批量添加好友
        /// </summary>
        /// <param name="chatUserFriendDTOs">好友对象集合</param>
        /// <returns></returns>
        Task AddFriendAsync(List<ChatUserFriendDTO> chatUserFriendDTOs);

        /// 单个删除好友
        /// </summary>
        /// <param name="chatUserFriendDTO">好友对象</param>
        /// <returns></returns>
        Task DeleteFriendAsync(ChatUserFriendDTO chatUserFriendDTO);

        /// 批量删除好友
        /// </summary>
        /// <param name="chatUserFriendDTOs">好友对象的id</param>
        /// <returns></returns>
        Task DeleteFriendAsync(List<ChatUserFriendDTO> chatUserFriendDTOs);

    }
}
