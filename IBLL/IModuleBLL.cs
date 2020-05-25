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
    public interface IModuleBLL
    {
        /// <summary>
        /// 单个模块新增
        /// </summary>
        /// <param name="roleDTO"></param>
        /// <returns></returns>
        Task AddAsync(ModuleDTO roleDTO);

        /// <summary>
        /// 单个删除模块
        /// </summary>
        /// <param name="roleDTO"></param>
        /// <returns></returns>
        Task DeleteAsync(ModuleDTO roleDTO);

        /// <summary>
        /// 单个删除模块
        /// </summary>
        /// <param name="roleDTO"></param>
        /// <returns></returns>
        Task Delete_TrueAsync(ModuleDTO roleDTO);

        /// <summary>
        /// 模块修改
        /// </summary>
        /// <param name="roleDTO"></param>
        /// <param name="actionNames"></param>
        /// <returns></returns>
        Task UpdateAsync(ModuleDTO module, string[] actionNames);

        /// <summary>
        /// 获取当前项目的所有的control
        /// </summary>
        /// <returns></returns>
        List<string> GetControls();

        /// <summary>
        /// 获取当前项目中的所有的action
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        List<string> GetActions(string ControlName);

        /// <summary>
        /// 模型新增
        /// </summary>
        /// <param name="module"></param>
        /// <param name="ActionNames"></param>
        Task AddAsync(ModuleDTO moduleDTO, string[] ActionNames);

        /// <summary>
        /// 获取要修改的module
        /// </summary>
        /// <param name="ModuleId"></param>
        /// <returns></returns>
        Task<ModuleViewDTO> GetUpdateModulesAsync(string ModuleId);

        /// <summary>
        /// 获取所有isdeleted标志为false的module
        /// </summary>
        /// <returns></returns>
        Task<List<ModuleDTO>> GetAllModuleDTOsAsync();

        /// <summary>
        /// 根据动态条件查询单个ModuleDTO
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        Task<ModuleDTO> GetEntityAsync(Dictionary<string, object> dict);

        /// <summary>
        /// 根据动态条件查询ModuleDTO集合
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        Task<List<ModuleDTO>> GetFilterAsync(Dictionary<string, object> dict);

        /// <summary>
        /// 根据动态条件删除指定的module
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        Task DeleteAsync(Dictionary<string, object> dict);

    }
}
