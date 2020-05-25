using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    /// <summary>
    /// 聊天用户和群组的数据传输对象(用于聊天对象的修改页面)
    /// </summary>
    public class ChatUserGroupDTO
    {
        /// <summary>
        /// 指定的聊天对象
        /// </summary>
        public ChatUserDTO ChatUserDTO { get; set; }
        /// <summary>
        /// 聊天对象对应的群组集合
        /// </summary>
        public List<GroupDTO> GroupDTOs { get; set; }
    }
}
