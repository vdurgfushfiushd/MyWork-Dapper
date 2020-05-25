using System;

namespace Model
{
    /// <summary>
    /// 日志类
    /// </summary>
    public class Note: IModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 笔记名字
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 笔记内容
        /// </summary>
        public string Detail { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } 
        /// <summary>
        /// 删除标记
        /// </summary>
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 笔记作者id
        /// </summary>
        public string User_Id { get; set; }
    }
}
