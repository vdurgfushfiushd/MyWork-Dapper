using System.Collections.Generic;

namespace DTO
{
    /// <summary>
    /// 展示所有歌曲的风格集合，主题集合，语言集合（用于Index页面的初始化填充）
    /// </summary>
    public class MusicListDTO
    {
        public List<string> Styles { get; set; }
        public List<string> Themes { get; set; }
        public List<string> Languages { get; set; }
        public List<MusicDTO> MusicDTOs { get; set; }
    }
}
