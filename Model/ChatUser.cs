using System;
using System.Collections.Generic;

namespace Model
{
    /// <summary>
    /// 聊天用户类
    /// </summary>
    [Serializable]
    public class ChatUser:IModel
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
        public DateTime CreateTime { get ; set; }
        /// <summary>
        /// 删除标记
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
