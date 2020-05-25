
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
    public class ChatBLL : IChatBLL
    {
        public IChatDAL chatDAL { get; set; }

        public IChatUserDAL chatUserDAL { get; set; }

        public IGroupDAL groupDAL { get; set; }

        public IChatUserFriendDAL chatUserFriendDAL { get; set; }

        public void Add(ChatUserDTO chatUserDTO)
        {
            throw new NotImplementedException();
        }

        public void Login(ChatUserDTO chatUserDTO)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取指定用户的联系人和所属群组
        /// </summary>
        /// <param name="chatUserDTO"></param>
        /// <returns></returns>
        public async Task<ChatIndexViewDTO> GetChatIndexViewDTOAsync(ChatUserDTO chatUserDTO)
        {
            ////指定的聊天对象
            //var db_chatUser = await chatUserDAL.GetEntityAsync(e=>e.Id== chatUserDTO.Id&&e.IsDeleted==false);
            ////聊天对象对应的好友集合
            //var db_friends = (await chatUserFriendDAL.GetFilterAsync(e => e.ChatUser_Id == db_chatUser.Id)).ToList();
            ////指定聊天对象的联系人(好友)
            //var ChatUserDTOs = new List<ChatUserDTO>();
            //foreach (var db_friend in db_friends)
            //{
            //    //friendid对应的聊天信息
            //    var db_chatuser = Mapper.Map<ChatUserDTO>(await chatUserDAL.GetEntityAsync(e => e.Id == db_friend.Friend_Id));
            //    ChatUserDTOs.Add(db_chatuser);
            //}
            ////指定聊天对象的群组
            //var GroupDTOs = db_chatUser.Groups.ToList().Select(e => Mapper.Map<GroupDTO>(e)).ToList();
            //return new ChatIndexViewDTO() { ChatUserDTO=Mapper.Map<ChatUserDTO>(db_chatUser),ChatUserDTOs = ChatUserDTOs, GroupDTOs = GroupDTOs };
            return null;
        }
    }
}
