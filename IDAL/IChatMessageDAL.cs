using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    public interface IChatMessageDAL
    {
        /// <summary>
        /// 单个ChatMessage新增
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Task AddAsync(ChatMessage t);

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="chatMessages"></param>
        /// <returns></returns>
        Task AddRangeAsync(List<ChatMessage> chatMessages);

        /// <summary>
        /// 单个ChatMessage删除
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Task DeleteAsync(ChatMessage chatMessage);

        /// <summary>
        /// 单个ChatMessage修改
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Task UpdateAsync(ChatMessage chatMessage);

        /// <summary>
        /// 单个ChatMessage查询
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Task<ChatMessage> GetEntityAsync(ChatMessage chatMessage);

        /// <summary>
        /// 查询指定发送者和接受者之间的所有聊天记录（单向信息）
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Task<List<ChatMessage>> GetFiltersAsync(ChatMessage chatMessage);

        /// <summary>
        /// 查询发送者和接受者之间的所有聊天记录(双向信息)
        /// </summary>
        /// <param name="chatMessage"></param>
        /// <returns></returns>
        Task<List<ChatMessage>> GetFiltersAllAsync(ChatMessage chatMessage);

        /// <summary>
        /// 查询指定发送者和接受者之间的指定日期的聊天记录
        /// </summary>
        /// <param name="t"></param>
        /// <param name="startTime"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        Task<List<ChatMessage>> GetFiltersAsync(ChatMessage chatMessage, DateTime startTime, DateTime dateTime);

        /// <summary>
        /// 模糊查询聊天消息集合
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        Task<List<ChatMessage>> GetFiltersAsync(string keyword);
    }
}
