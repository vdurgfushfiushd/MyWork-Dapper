using AutoMapper;
using DTO;
using Model;

namespace MyWork.AutoMapperProfile
{
    public class ChatUserMapperProfile : Profile
    {
        public ChatUserMapperProfile()
        {
            CreateMap<ChatUser, ChatUserDTO>();
            CreateMap<ChatUserDTO, ChatUser>();
            //下面还可以加上其他的映射文件
        }
    }
}