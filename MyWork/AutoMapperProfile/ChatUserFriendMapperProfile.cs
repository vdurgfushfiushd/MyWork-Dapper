using AutoMapper;
using DTO;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWork.AutoMapperProfile
{
    public class ChatUserFriendMapperProfile: Profile
    {
        public ChatUserFriendMapperProfile()
        {
            CreateMap<ChatUserFriend, ChatUserFriendDTO>();
            CreateMap<ChatUserFriendDTO, ChatUserFriend>();
            //下面还可以加上其他的映射文件
        }
    }
}