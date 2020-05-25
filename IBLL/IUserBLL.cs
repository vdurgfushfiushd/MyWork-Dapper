using DTO;
using Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IBLL
{
    /// <summary>
    /// 用户对象的业务逻辑层类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IUserBLL
    {


        /// <summary>
        /// 单个用户新增
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        Task AddAsync(UserDTO userDTO);


        /// <summary>
        /// 单个删除(软删除)
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        Task DeleteAsync(UserDTO userDTO);


        /// <summary> 
        /// 单个真实删除
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        Task Delete_TrueAsync(UserDTO userDTO);


        /// <summary>
        /// 单个用户信息修改
        /// </summary>
        /// <param name="userDTO"></param>
        /// <param name="RoleIds"></param>
        /// <returns></returns>
        Task UpdateAsync(UserDTO userDTO, string[] RoleIds);


        /// <summary>
        /// 单个UserRoleDTO查找
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<UserRoleDTO> GetUpdateEntityAsync(string Id);

        /// <summary>
        /// 判断用户是否拥有该权限
        /// </summary>
        /// <param name="Id">用户id</param>
        /// <param name="ControllerName">控制器名字</param>
        /// <param name="ActionName">action名字</param>
        /// <returns></returns>
        Task<bool> GetPermissionFlagAsync(string Id, string ControllerName, string ActionName);

        /// <summary>
        /// 获取用户对应的角色，模块集合
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<List<UserRoleModuleDTO>> GetUserRoleModuleAsync(string Id);

        /// <summary>
        /// 获取所有isdeleted标志为false的user
        /// </summary>
        /// <returns></returns>
        Task<List<UserDTO>> GetAllUserDTOsAsync();

        /// <summary>
        /// 根据用户名和密码判断该用户是否存在
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        Task<UserDTO> LoginAsync(string Name,string Password);

        /// <summary>
        /// 根据id删除user
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task DeleteByIdAsync(string Id);

        /// <summary>
        /// 根据动态条件查询单个user
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        Task<UserDTO> GetEntityAsync(Dictionary<string, object> dict);

        /// <summary>
        /// 根据动态条件查询user集合
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        Task<List<UserDTO>> GetFilterAsync(Dictionary<string, object> dict);

        /// <summary>
        /// 根据动态条件删除指定的user
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        Task DeleteAsync(Dictionary<string, object> dict);

    }
}
