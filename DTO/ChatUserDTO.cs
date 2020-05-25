using System;

namespace DTO
{
    /// <summary>
    /// 聊天用户的数据传输对象
    /// </summary>
    public class ChatUserDTO
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 聊天对象的connectionId
        /// </summary>
        public string ConnectionId { get; set; }
        /// <summary>
        /// 对象名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 聊天对象密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 是否在线
        /// </summary>
        public bool IsOnline { get; set; } 
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 是否被选择
        /// </summary>
        public bool IsChoosed { get; set; }
    }
}
