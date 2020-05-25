using AutoMapper;
using DTO;
using Model;

namespace MyWork.AutoMapperProfile
{
    public class SingerMapperProfile : Profile
    {
        public SingerMapperProfile()
        {
            CreateMap<Singer, SingerDTO>();
            CreateMap<SingerDTO, Singer>();
            //下面还可以加上其他的映射文件
        }
    }
}