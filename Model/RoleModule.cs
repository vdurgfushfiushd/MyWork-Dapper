using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 角色模块关联类
    /// </summary>
    public class RoleModule
    {
        /// <summary>
        /// role的主键
        /// </summary>
        public string Role_Id { get; set; }
        /// <summary>
        /// 对应的role
        /// </summary>
        public Role Role { get; set; }
        /// <summary>
        /// module的主键
        /// </summary>
        public string Module_Id { get; set; }
        /// <summary>
        /// 对应的module
        /// </summary>
        public Module Module { get; set; }
    }
}
