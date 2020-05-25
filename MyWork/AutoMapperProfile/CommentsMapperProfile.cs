using AutoMapper;
using DTO;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWork.AutoMapperProfile
{
    public class CommentsMapperProfile : Profile
    {
        public CommentsMapperProfile()
        {
            CreateMap<Comments, CommentsDTO>();
            CreateMap<CommentsDTO, Comments>();
            //下面还可以加上其他的映射文件
        }
    }
}