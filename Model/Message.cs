using System;

namespace Model
{
    /// <summary>
    /// 发送的消息类
    /// </summary>
    public class Message
    {
        /// <summary>
        /// 发送者
        /// </summary>
        public string Sender { get; set; }
        /// <summary>
        /// 接收者
        /// </summary>
        public string Receiver { get; set; }
        /// <summary>
        /// 发送消息
        /// </summary>
        public string SendMessage { get; set; }
        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime SendTime { get; set; }
    }
}
