using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    /// <summary>
    /// 用户及其对应的角色类(用于用户修改页面的显示)
    /// </summary>
    public class UserRoleViewDTO
    {
        /// <summary>
        /// 用户信息 
        /// </summary>
        public UserDTO UserDTO { get; set; }
        /// <summary>
        /// 用户对应的角色
        /// </summary>
        public List<RoleDTO> RoleDTOs { get; set; }
    }
}
