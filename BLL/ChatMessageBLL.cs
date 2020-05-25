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
    public class ChatMessageBLL : IChatMessageBLL
    {
        public IUserDAL userDAL { get; set; }

        public IRoleDAL roleDAL { get; set; }

        public IModuleDAL powerDAL { get; set; }

        public INoteDAL noteDAL { get; set; }

        public IChatMessageDAL chatMessageDAL { get; set; }

        public IGroupDAL groupDAL { get; set; }

        /// <summary>
        /// 新增指定人员发送到指定接收人员的消息
        /// </summary>
        /// <param name="chatMessageDTO"></param>
        /// <returns></returns>
        public async Task AddAsync(ChatMessageDTO chatMessageDTO)
        {
            chatMessageDTO.Id = Guid.NewGuid().ToString();
            chatMessageDTO.IsDeleted = false;
            chatMessageDTO.CreateTime = DateTime.Now;
            var chatmessage = Mapper.Map<ChatMessage>(chatMessageDTO);
            await chatMessageDAL.AddAsync(chatmessage);
        }

        /// <summary>
        /// 删除指定人员发送的消息
        /// </summary>
        /// <param name="chatMessageDTO"></param>
        /// <returns></returns>
        public async Task DeleteAsync(ChatMessageDTO chatMessageDTO)
        {
            var chatmessage = Mapper.Map<ChatMessage>(chatMessageDTO);
            await chatMessageDAL.DeleteAsync(chatmessage);
        }

        /// <summary>
        /// 聊天消息修改
        /// </summary>
        /// <param name="chatMessageDTO"></param>
        /// <returns></returns>
        public async Task UpdateAsync(ChatMessageDTO chatMessageDTO)
        {
            var chatmessage = Mapper.Map<ChatMessage>(chatMessageDTO);
            await chatMessageDAL.UpdateAsync(chatmessage);
        }

        /// <summary>
        /// 聊天消息集合查询(单向信息)
        /// </summary>
        /// <param name="chatMessageDTO"></param>
        /// <returns></returns>
        public async Task<List<ChatMessageDTO>> GetFiltersAsync(ChatMessageDTO chatMessageDTO)
        {
            var chatmessage = Mapper.Map<ChatMessage>(chatMessageDTO);
            var list = await chatMessageDAL.GetFiltersAsync(chatmessage);
            return list.Select(e=>Mapper.Map<ChatMessageDTO>(e)).ToList();
        }

        /// <summary>
        /// 查询发送者和接受者之间的所有聊天记录(双向信息)
        /// </summary>
        /// <param name="chatMessage"></param>
        /// <returns></returns>
        public async Task<List<ChatMessageDTO>> GetFiltersAllAsync(ChatMessageDTO chatMessageDTO)
        {
            var chatmessage = Mapper.Map<ChatMessage>(chatMessageDTO);
            var list = await chatMessageDAL.GetFiltersAllAsync(chatmessage);
            return list.Select(e => Mapper.Map<ChatMessageDTO>(e)).ToList();
        }

        /// <summary>
        /// 单个聊天消息查询
        /// </summary>
        /// <param name="chatMessageDTO"></param>
        /// <returns></returns>
        public async Task<ChatMessageDTO> GetEntityAsync(ChatMessageDTO chatMessageDTO)
        {
            var chatmessage = Mapper.Map<ChatMessage>(chatMessageDTO);
            var result= await chatMessageDAL.GetEntityAsync(chatmessage);
            return Mapper.Map<ChatMessageDTO>(result);
        }

        /// <summary>
        /// 指定日期的聊天消息集合查询
        /// </summary>
        /// <param name="chatMessageDTO"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public async Task<List<ChatMessageDTO>> GetFiltersAsync(ChatMessageDTO chatMessageDTO, DateTime startTime, DateTime endTime)
        {
            var chatmessage = Mapper.Map<ChatMessage>(chatMessageDTO);
            var list = await chatMessageDAL.GetFiltersAsync(chatmessage,startTime, endTime);
            return list.Select(e=>Mapper.Map<ChatMessageDTO>(e)).ToList();
        }

        /// <summary>
        /// 获取群组成员之间的聊天信息集合
        /// </summary>
        /// <param name="GroupId"></param>
        /// <returns></returns>
        public async Task<List<ChatMessageDTO>> GetChatMessageByGroup(string GroupId)
        {
            ////数据库中指定的群组
            //var db_group = await groupDAL.GetEntityAsync(e=>e.Id==GroupId&&e.IsDeleted==false);
            ////群组对应的消息集合
            //var list=(await chatMessageDAL.GetFiltersAsync(db_group.GroupName)).ToList().Select(e=>Mapper.Map<ChatMessageDTO>(e)).ToList();
            //return list;
            return null;
        }
    }
}
