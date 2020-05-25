using System.Collections.Generic;

namespace DTO
{
    /// <summary>
    /// 用户及其对应的角色类(用于用户修改页面的显示)
    /// </summary>
    public class UserRoleDTO
    {
        public UserDTO UserDTO { get; set; }

        public List<RoleDTO> RoleDTOs { get; set; } 
    }
}
