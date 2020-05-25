using System;

namespace Model
{
    /// <summary>
    /// 聊天记录类
    /// </summary>
    [Serializable]
    public class ChatMessage
    {
        /// <summary>
        /// 主键id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 发送的消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 发送者
        /// </summary>
        public string Sender { get; set; }
        /// <summary>
        /// 接受者
        /// </summary>
        public string Receiver { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
