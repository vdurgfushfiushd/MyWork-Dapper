
using System;
using System.Collections.Generic;

namespace Model
{
    /// <summary>
    /// 歌曲类
    /// </summary>
    public class Music : IModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 歌曲名字
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 语种
        /// </summary>
        public string Language { get; set; }
        /// <summary>
        /// 歌曲类型
        /// </summary>
        public string Style { get; set; }
        /// <summary>
        /// 主题
        /// </summary>
        public string Theme { get; set; }
        /// <summary>
        /// 歌词
        /// </summary>
        public string Lyrics { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 歌曲地址
        /// </summary>
        public string Url { get; set; }
    }
}
  