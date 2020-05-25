using System.Collections.Generic;

namespace DTO
{
    //展示所有歌手的国家集合，风格集合（用于SingerList页面的初始化填充）
    public class SingerListDTO
    {
        public List<string> Countrys { get; set; }
        public List<string> Styles { get; set; }
        public List<SingerDTO> singerDTOs { get; set; }
    }
}
