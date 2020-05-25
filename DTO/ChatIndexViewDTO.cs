using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    /// <summary>
    /// 聊天首页数据传输对象(用于显示聊天用户首页的数据及聊天内容)
    /// </summary>
    public class ChatIndexViewDTO
    {
        /// <summary>
        /// 登陆的聊天用户
        /// </summary>
        public ChatUserDTO ChatUserDTO { get; set; }
        /// <summary>
        /// 聊天对象的联系人集合
        /// </summary>
        public List<ChatUserDTO> ChatUserDTOs { get; set; }
        /// <summary>
        /// 聊天对象所属的群组
        /// </summary>
        public List<GroupDTO> GroupDTOs { get; set; }
    }
}
