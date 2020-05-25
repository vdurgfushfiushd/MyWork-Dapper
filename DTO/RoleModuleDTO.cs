using System.Collections.Generic;

namespace DTO
{
    /// <summary>
    /// 角色及其对应的模块类
    /// </summary>
    public class RoleModuleDTO
    {
        /// <summary>
        /// 角色DTO
        /// </summary>
        public RoleDTO RoleDTO { get; set; }
        /// <summary>
        /// 角色对应的模块集合
        /// </summary>
        public List<ModuleDTO> ModuleDTOs { get; set; }
    }
}
