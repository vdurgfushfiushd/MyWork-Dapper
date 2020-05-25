using System;
using System.Collections.Generic;

namespace Model
{
    /// <summary>
    /// 歌手类
    /// </summary>
    public class Singer : IModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 歌手名字
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 歌手名字首字母
        /// </summary>
        public string FirstLetter{get;set;}
        /// <summary>
        /// 风格
        /// </summary>
        public string Style { get; set; }
        /// <summary>
        /// 歌手国家
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }
        /// <summary>
        /// 照片
        /// </summary>
        public string Picture { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
