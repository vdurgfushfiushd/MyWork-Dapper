
using Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IDAL
{
    public interface IChatUserDAL : IDapperHelperDAL<ChatUser>
    {

        /// <summary>
        /// 获取在线用户
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<ChatUser>> GetOnlineAsync();

        /// <summary>
        /// 向指定用户发送消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task SendMessageAsync(Message message);

        /// <summary>
        /// 获取指定用户到接收者之间的聊天消息
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="Receiver"></param>
        /// <returns></returns>
        Task<IEnumerable<Message>> GetSendMessageAsync(string Sender,string Receiver);

        ////根据ChatName获取用户的ConnectionId
        //string GetConnectionId(string ChatName);

        /// <summary>
        /// 创建群组
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        Task CreateGroupAsync(Group group);

        /// <summary>
        /// 对象所属的群组的集合
        /// </summary>
        /// <param name="ChatName"></param>
        /// <returns></returns>
        Task<IEnumerable<Group>> GetGroupAsync(string ChatName);
    }
}
