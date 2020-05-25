using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 用户角色表
    /// </summary>
    public class Role : IModel
    {
        /// <summary>
        /// 主键(guid类型)
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 角色名字
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 角色描述
        /// </summary>
        public string Describe { get; set; }
    }
}
