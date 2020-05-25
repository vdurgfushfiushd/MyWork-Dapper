using AutoMapper;
using DTO;
using IBLL;
using IDAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BLL
{
    public class ChatUserBLL : IChatUserBLL
    {
        public IChatUserDAL chatUserDAL { get; set; }

        public IChatUserGroupDAL chatUserGroupDAL { get; set; }

        public IGroupDAL groupDAL { get; set; }

        public IChatUserFriendDAL chatUserFriendDAL { get; set; }

        /// <summary>
        /// 用户新增
        /// </summary>
        /// <param name="chatUserDTO"></param>
        /// <returns></returns>
        public async Task AddAsync(ChatUserDTO chatUserDTO)
        {
            //数据库中对应的数据
            string sql1 = @"select * from t_chatusers where Id=@Id and IsDeleted=false";
            var db_chatUser = await chatUserDAL.GetEntityAsync(sql1,chatUserDTO);
            //数据库中没有该数据，则新增
            if (db_chatUser == null)
            {
                //要新增的数据
                var chatUser = Mapper.Map<ChatUser>(chatUserDTO);
                //设置属性
                chatUser.Id = Guid.NewGuid().ToString("n");
                chatUser.IsOnline = false;
                chatUser.IsDeleted = false;
                chatUser.CreateTime = DateTime.Now;
                //用户新增
                string sql2 = @"insert into t_chatusers(Id,ConnectionId,Name,Password,IsOnline,CreateTime,IsDeleted) values(@Id,@ConnectionId,@Name,@Password,@IsOnline,@CreateTime,@IsDeleted)";
                await chatUserDAL.ExecuteAsync(sql2,chatUser);
            }
        }

        /// <summary>
        /// 单个删除(软删除)
        /// </summary>
        /// <param name="chatUserDTO"></param>
        /// <returns></returns>
        public async Task DeleteAsync(ChatUserDTO chatUserDTO)
        {
            //数据库中对应的数据
            string sql1 = @"select * from t_chatusers where Id=@Id and IsDeleted=false";
            var db_chatUser = await chatUserDAL.GetEntityAsync(sql1,chatUserDTO);
            //数据库中有该数据，则删除
            if (db_chatUser != null)
            {
                //设置删除表即为true
                string sql2 = @"update t_chatusers set IsDeleted=true where Id=@Id";
                await chatUserDAL.ExecuteAsync(sql2,db_chatUser);
            }
        }

        /// <summary>
        /// 动态条件单个删除(软删除)
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Expression<Func<ChatUser, bool>> exp)
        {

        }

        /// <summary>
        /// 单个删除(真实删除)
        /// </summary>
        /// <param name="chatUserDTO"></param>
        /// <returns></returns>
        public async Task Delete_TrueAsync(ChatUserDTO chatUserDTO)
        {
            //数据库中对应的数据
            string sql1 = @"select * from t_chatusers where Id=@Id and IsDeleted=false";
            var db_chatUser = await chatUserDAL.GetEntityAsync(sql1,chatUserDTO);
            //数据库中有该数据，则删除
            if (db_chatUser != null)
            {
                //删除聊天用户和群组之间的关系数据  
                string sql2 = @"delete from t_chatusergrouprelations where ChatUser_Id=@Id";
                await chatUserGroupDAL.ExecuteAsync(sql2,chatUserDTO);
                //删除聊天用户表中的数据
                var chatUser = Mapper.Map<ChatUser>(db_chatUser);
                string sql3 = @"delete from t_chatusers where Id=@Id";
                await chatUserDAL.ExecuteAsync(sql3,chatUser);
            }
        }

        /// <summary>
        /// 动态条件删除(真实删除)
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public async Task Delete_TrueAsync(Expression<Func<ChatUser, bool>> exp)
        {

        }

        /// <summary>
        /// 单个修改
        /// </summary>
        /// <param name="chatUserDTO"></param>
        /// <returns></returns>
        public async Task UpdateAsync(ChatUserDTO chatUserDTO, string[] Group_Ids)
        {
            ////数据库中对应聊天对象的数据
            //string sql1 = "select * from t_chatusers where Id=@Id and IsDeleted=false";
            //var db_chatuser = await chatUserDAL.GetEntityAsync(sql1,chatUserDTO);
            ////删除聊天对象和群组关系表中的数据
            //string sql2 = "delete from t_chatusergrouprelations where ChatUser_Id=@Id";
            //await chatUserGroupDAL.ExecuteAsync(sql2,chatUserDTO);
            ////新增新的聊天对象和群组关系数据
            //foreach (var Group_Id in Group_Ids)
            //{
            //    var db_group = await groupDAL.GetEntityAsync(e=>e.Id== Group_Id&&e.IsDeleted==false);
            //    db_chatuser.Groups.Add(db_group);
            //}
            ////修改聊天对象的属性
            //db_chatuser.Name = chatUserDTO.Name;
            //db_chatuser.Password = chatUserDTO.Password;
            ////事务提交
            //await chatUserDAL.CommitAsync();
        }

        /// <summary>
        /// 动态条件单个查询
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public async Task<ChatUserDTO> GetEntityAsync(Expression<Func<ChatUser, bool>> exp)
        {
            return null;
        }

        /// <summary>
        /// 动态条件多个查询
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public async Task<List<ChatUserDTO>> GetFilterAsync(Expression<Func<ChatUser, bool>> exp)
        {
            return null;
        }

        /// <summary>
        /// 获取在线用户
        /// </summary>
        /// <returns></returns>
        public async Task<List<ChatUserDTO>> GetOnlineAsync()
        {
            var list = await chatUserDAL.GetOnlineAsync();
            return list.Select(e=>Mapper.Map<ChatUserDTO>(e)).ToList();
        }

        /// <summary>
        /// 用户退出
        /// </summary>
        /// <param name="CurrentChatName"></param>
        /// <returns></returns>
        public async Task ExitAsync(Expression<Func<ChatUser, bool>> exp)
        {
           
        }

        /// <summary>
        /// 向指定的用户发送消息
        /// </summary>
        /// <param name="messageDTO"></param>
        /// <returns></returns>
        public async Task SendMessageAsync(MessageDTO messageDTO)
        {
            var message = Mapper.Map<Message>(messageDTO);
            await chatUserDAL.SendMessageAsync(message);
        }

        /// <summary>
        /// 获取指定用户到接收者之间的聊天信息
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="Receiver"></param>
        /// <returns></returns>
        public async Task<List<MessageDTO>> GetSendMessageAsync(string Sender, string Receiver)
        {
            var messages = await chatUserDAL.GetSendMessageAsync(Sender,Receiver);
            return messages.Select(e=>Mapper.Map<MessageDTO>(e)).ToList();
        }

        /// <summary>
        /// 创建群组
        /// </summary>
        /// <param name="groupDTO"></param>
        /// <returns></returns>
        public async Task CreateGroupAsync(GroupDTO groupDTO)
        {
            var group = Mapper.Map<Group>(groupDTO);
            await chatUserDAL.CreateGroupAsync(group);
        }

        /// <summary>
        /// 对象所属的群组的集合
        /// </summary>
        /// <param name="ChatName"></param>
        /// <returns></returns>
        public async Task<List<GroupDTO>> GetGroupAsync(string ChatName)
        {
            return (await chatUserDAL.GetGroupAsync(ChatName)).ToList().Select(e=>Mapper.Map<GroupDTO>(e)).ToList();
        }

        /// <summary>
        /// 获取要修改的chatuser
        /// </summary>
        /// <param name="ModuleId"></param>
        /// <returns></returns>
        public async Task<ChatUserGroupDTO> GetUpdateChatUserAsync(string Id)
        {
            ////数据库中的对应的聊天对象
            //var db_chatuser = await chatUserDAL.GetEntityAsync(e => e.Id == Id && e.IsDeleted == false);
            ////聊天对象对应的群组集合
            //var db_groups = db_chatuser.Groups.ToList();
            ////数据库当中所有的群组集合
            //var db_groups_all = (await groupDAL.GetFilterAsync(e => e.IsDeleted == false)).ToList().Select(e => Mapper.Map<GroupDTO>(e)).ToList();
            ////标记聊天对象对应的群组
            //foreach (var db_group in db_groups_all)
            //{
            //    //若聊天对象群组关系表中有该群组，则标记该群组
            //    if (db_groups.Select(e => e.Id).Contains(db_group.Id))
            //    {
            //        db_group.IsChoosed = true;
            //    }
            //    else
            //    {
            //        db_group.IsChoosed = false;
            //    }
            //}
            //return new ChatUserGroupDTO() { ChatUserDTO = Mapper.Map<ChatUserDTO>(db_chatuser), GroupDTOs = db_groups_all };
            return null;
        }

        /// <summary>
        /// 聊天用户登录
        /// </summary>
        /// <param name="chatUserDTO"></param>
        /// <returns></returns>
        public async Task<ChatUserDTO> ChatUserLogin(ChatUserDTO chatUserDTO)
        {
            ////数据库中对应的聊天对象
            //var chatuser = await chatUserDAL.GetEntityAsync(e=>e.Name == chatUserDTO.Name&&e.Password == chatUserDTO.Password&&e.IsDeleted == false);
            ////如果数据库中有该聊天对象，则将该聊天对象的在线状态设置为true，并返回改聊天对象
            //if (chatuser != null)
            //{
            //    chatuser.IsOnline = true;
            //    await chatUserDAL.CommitAsync();
            //    return Mapper.Map<ChatUserDTO>(chatuser);
            //}
            //else
            //{
            //    return null;
            //}
            return null;
        }

        /// <summary>
        /// 聊天用户加入指定的群组
        /// </summary>
        /// <param name="chatUserDTO"></param>
        /// <param name="groupDTO"></param>
        /// <returns></returns>
        public async Task JoinGroupAsync(ChatUserDTO chatUserDTO, GroupDTO groupDTO)
        {
            ////数据库中对应的聊天用户
            //var db_chatuser = await chatUserDAL.GetEntityAsync(e => e.Id == chatUserDTO.Id && e.IsDeleted == false);
            ////数据库中对应的群组
            //var db_group = await groupDAL.GetEntityAsync(e=>e.GroupName== groupDTO.GroupName && e.IsDeleted==false);
            ////将指定的聊天用户对象加入到群组当中(聊天用户和群组关系表中新增一条数据)
            //db_group.ChatUsers.Add(db_chatuser);
            //await groupDAL.CommitAsync();
        }

        /// <summary>
        /// 单个添加好友
        /// </summary>
        /// <param name="chatUserFriendDTO">添加人的id</param>
        /// <returns></returns>
        public async Task AddFriendAsync(ChatUserFriendDTO chatUserFriendDTO)
        {
            //数据库表中对应的好友对象
            var db_chatuserfriend = await chatUserFriendDAL.GetEntityAsync(e=>e.ChatUser_Id== chatUserFriendDTO.ChatUser_Id&&e.Friend_Id== chatUserFriendDTO.Friend_Id);
            //数据库中没有，则新增
            if (db_chatuserfriend == null)
            {
                var chatuserfriend = Mapper.Map<ChatUserFriend>(chatUserFriendDTO);
                await chatUserFriendDAL.AddAsync(chatuserfriend);
                //await chatUserFriendDAL.CommitAsync();
            }
        }

        /// <summary>
        /// 批量添加好友
        /// </summary>
        /// <param name="chatUserFriendDTOs">好友对象集合</param>
        /// <returns></returns>
        public async Task AddFriendAsync(List<ChatUserFriendDTO> chatUserFriendDTOs)
        {
            foreach (var chatUserFriendDTO in chatUserFriendDTOs)
            {
                //数据库中对应的数据
                var db_chatuserfriend= await chatUserFriendDAL.GetEntityAsync(e => e.ChatUser_Id == chatUserFriendDTO.ChatUser_Id && e.Friend_Id == chatUserFriendDTO.Friend_Id);
                //数据库中没有该数据，则可以新增
                if (db_chatuserfriend == null)
                {
                    var chatuserfriend = Mapper.Map<ChatUserFriend>(chatUserFriendDTO);
                    await chatUserFriendDAL.AddAsync(chatuserfriend);
                }
            }
            //await chatUserFriendDAL.CommitAsync();
        }

        /// 单个删除好友
        /// </summary>
        /// <param name="chatUserFriendDTO">好友对象</param>
        /// <returns></returns>
        public async Task DeleteFriendAsync(ChatUserFriendDTO chatUserFriendDTO)
        {
            //数据库表中对应的好友对象
            var db_chatuserfriend = await chatUserFriendDAL.GetEntityAsync(e => e.ChatUser_Id == chatUserFriendDTO.ChatUser_Id && e.Friend_Id == chatUserFriendDTO.Friend_Id);
            //数据库中有，则删除
            if (db_chatuserfriend != null)
            {
                await chatUserFriendDAL.DeleteAsync(db_chatuserfriend);
                //await chatUserFriendDAL.CommitAsync();
            }
        }

        /// 批量删除好友
        /// </summary>
        /// <param name="chatUserFriendDTOs">好友对象的id</param>
        /// <returns></returns>
        public async Task DeleteFriendAsync(List<ChatUserFriendDTO> chatUserFriendDTOs)
        {
            foreach (var chatUserFriendDTO in chatUserFriendDTOs)
            {
                //数据库中对应的数据
                var db_chatuserfriend = await chatUserFriendDAL.GetEntityAsync(e => e.ChatUser_Id == chatUserFriendDTO.ChatUser_Id && e.Friend_Id == chatUserFriendDTO.Friend_Id);
                //数据库中有该数据，则可以删除
                if (db_chatuserfriend != null)
                {
                    await chatUserFriendDAL.AddAsync(db_chatuserfriend);
                }
            }
            //await chatUserFriendDAL.CommitAsync();
        }
    }
}
