using DTO;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    /// <summary>
    /// 角色对象的业务逻辑层类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRoleBLL
    {
        /// <summary>
        /// 单个角色新增
        /// </summary>
        /// <param name="roleDTO"></param>
        /// <returns></returns>
        Task AddAsync(RoleDTO roleDTO);
      
        /// <summary>
        /// 单个角色删除(软删除)
        /// </summary>
        /// <param name="roleDTO"></param>
        /// <returns></returns>
        Task DeleteAsync(RoleDTO roleDTO);
      
        /// <summary>
        /// 单个角色删除
        /// </summary>
        /// <param name="roleDTO"></param>
        /// <returns></returns>
        Task Delete_TrueAsync(RoleDTO roleDTO);
      
        /// <summary>
        /// 角色信息修改
        /// </summary>
        /// <param name="roleDTO"></param>
        /// <param name="ModuleIds"></param>
        /// <returns></returns>
        Task UpdateAsync(RoleDTO roleDTO, string[] ModuleIds);
       
        /// <summary>
        /// 获取要修改的角色及其对应的模块
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<RoleModuleDTO> GetUpdateEntityAsync(string Id);

        /// <summary>
        /// 获取指定角色及其模块
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<RoleModuleDTO> GetRoleModuleAsync(string Id);

        /// <summary>
        /// 获取所有isdeleted标志为false的role
        /// </summary>
        /// <returns></returns>
        Task<List<RoleDTO>> GetAllRoleDTOsAsync();

        /// <summary>
        /// 根据动态条件查询单个RoleDTO
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        Task<RoleDTO> GetEntityAsync(Dictionary<string, object> dict);

        /// <summary>
        /// 根据动态条件查询RoleDTO集合
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        Task<List<RoleDTO>> GetFilterAsync(Dictionary<string, object> dict);

        /// <summary>
        /// 根据动态条件删除指定的role
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        Task DeleteAsync(Dictionary<string, object> dict);
    }
}
