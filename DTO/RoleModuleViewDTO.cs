using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    /// <summary>
    /// 角色及其对应的模型类(用于角色修改页面的显示)
    /// </summary>
    public class RoleModuleViewDTO
    {
        /// <summary>
        /// 角色信息
        /// </summary>
        public RoleDTO RoleDTO { get; set; }
        /// <summary>
        /// 角色对应的模型
        /// </summary>
        public List<ModuleDTO> ModuleDTOs { get; set; } 
    }
}
