using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    /// <summary>
    /// 聊天群组数据传输对象
    /// </summary>
    public class GroupDTO
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 群组名
        /// </summary>
        public string GroupName { get; set; }
        /// <summary>
        /// 群组简述
        /// </summary>
        public string Describe{ get; set; }
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
