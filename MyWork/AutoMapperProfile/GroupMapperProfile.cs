using AutoMapper;
using DTO;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWork.AutoMapperProfile
{
    public class GroupMapperProfile: Profile
    {
        public GroupMapperProfile()
        {
            CreateMap<Group, GroupDTO>();
            CreateMap<GroupDTO, Group>();
        }
    }
}