using System;

namespace DTO
{
    public class SingerDTO
    {
        public string Id { get; set; }
        //歌手名字
        public string Name { get; set; }
        //歌手名字首字母
        public string FirstLetter { get; set; }
        //歌手国家
        public string Country { get; set; }
        //风格
        public string Style { get; set; }
        //性别
        public string Sex { get; set; }
        //照片
        public string Picture { get; set; }
        //删除标记
        public bool? IsDeleted { get; set; }
        //创建时间
        public DateTime? CreateTime { get; set; }
    }
}
