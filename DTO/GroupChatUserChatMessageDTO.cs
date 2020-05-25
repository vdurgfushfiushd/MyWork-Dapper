using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    /// <summary>
    /// 群组及其对应的聊天用户集合,聊天信息集合
    /// </summary>
    public class GroupChatUserChatMessageDTO
    {
        /// <summary>
        /// 对应的群组
        /// </summary>
        public GroupDTO GroupDTO { get; set; }
        /// <summary>
        /// 群组对应的聊天用户集合
        /// </summary>
        public List<ChatUserDTO> ChatUserDTOs { get; set; }
        /// <summary>
        /// 群组对应的聊天信息集合
        /// </summary>
        public List<ChatMessageDTO> ChatMessageDTOs { get; set; }
    }
}
