using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 用户角色关联类
    /// </summary>
    public class UserRole
    {
        /// <summary>
        /// user的主键
        /// </summary>
        public string User_Id { get; set; }
        /// <summary>
        /// 对应的user
        /// </summary>
        public User User { get; set; }
        /// <summary>
        /// role的主键
        /// </summary>
        public string Role_Id { get; set; }
        /// <summary>
        /// 对应的role
        /// </summary>
        public Role Role { get; set; }
    }
}
