using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 聊天用户的好友类
    /// </summary>
    public class ChatUserFriend
    {
        /// <summary>
        /// 聊天用户的id
        /// </summary>
        public string ChatUser_Id { get; set; }
        /// <summary>
        /// 好友id
        /// </summary>
        public string Friend_Id { get; set; }
    }
}
