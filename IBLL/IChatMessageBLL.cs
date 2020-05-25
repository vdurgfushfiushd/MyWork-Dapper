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
    /// 聊天消息对象的业务逻辑层类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IChatMessageBLL
    {
        //单个ChatMessage新增
        Task AddAsync(ChatMessageDTO t);

        /// <summary>
        /// 单个ChatMessage删除
        /// </summary>
        /// <param name="chatMessageDTO"></param>
        /// <returns></returns>
        Task DeleteAsync(ChatMessageDTO chatMessageDTO);

        /// <summary>
        /// 单个ChatMessage修改
        /// </summary>
        /// <param name="chatMessageDTO"></param>
        /// <returns></returns>
        Task UpdateAsync(ChatMessageDTO chatMessageDTO);

        /// <summary>
        /// 单个ChatMessage查询
        /// </summary>
        /// <param name="chatMessageDTO"></param>
        /// <returns></returns>
        Task<ChatMessageDTO> GetEntityAsync(ChatMessageDTO chatMessageDTO);

        /// <summary>
        /// 查询指定发送者和接受者之间的所有聊天记录(单向信息)
        /// </summary>
        /// <param name="chatMessageDTO"></param>
        /// <returns></returns>
        Task<List<ChatMessageDTO>> GetFiltersAsync(ChatMessageDTO chatMessageDTO);

        /// <summary>
        /// 查询发送者和接受者之间的所有聊天记录(双向信息)
        /// </summary>
        /// <param name="chatMessage"></param>
        /// <returns></returns>
        Task<List<ChatMessageDTO>> GetFiltersAllAsync(ChatMessageDTO chatMessageDTO);

        /// <summary>
        /// 查询指定发送者和接受者之间的指定日期的聊天记录
        /// </summary>
        /// <param name="chatMessageDTO"></param>
        /// <param name="startTime"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        Task<List<ChatMessageDTO>> GetFiltersAsync(ChatMessageDTO chatMessageDTO, DateTime startTime,DateTime dateTime);

        /// <summary>
        /// 获取群组之间的聊天信息集合
        /// </summary>
        /// <param name="GroupId"></param>
        /// <returns></returns>
        Task<List<ChatMessageDTO>> GetChatMessageByGroup(string GroupId);
    }
}
