using System;
using DTO;
using IBLL;
using IDAL;
using Model;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using System.Data;

namespace BLL
{
    public class UserBLL : IUserBLL
    {
        public INoteDAL noteDAL { get; set; }

        public IUserDAL userDAL { get; set; }

        public IUserRoleDAL userRoleDAL { get; set; }

        public IRoleDAL roleDAL { get; set; }

        public IModuleDAL moduleDAL { get; set; }

        /// <summary>
        /// 单个用户新增
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        public async Task AddAsync(UserDTO userDTO)
        {
            try
            {
                //数据库中对应的用户
                var sql1 = @"select * from t_users where Id=@Id limit 0,1";
                var db_user = await userDAL.GetEntityAsync(sql1, new { Id= userDTO.Id});
                //数据库中没有数据，则可以新增
                if (db_user == null)
                {
                    string sql2 = @"insert into t_users(Id,Name,Password,Height,CreateTime,`Describe`,IsDeleted) values(@Id,@Name,@Password,@Height,@CreateTime,@Describe,@IsDeleted)";
                    await userDAL.ExecuteAsync(sql2, userDTO);
                }
                userDAL.Commit();
            }
            catch (Exception ex)
            {
                userDAL.Rollback();
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 单个用户删除(软删除)
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        public async Task DeleteAsync(UserDTO userDTO)
        {
            try
            {
                //数据库中对应的用户
                var sql1 = @"select * from t_users where Id=@Id";
                var db_user = await userDAL.GetEntityAsync(sql1, userDTO);
                //数据库中有数据，则可以删除
                if (db_user != null)
                {
                    var sql2 = "update t_users set IsDeleted=true where Id=@Id";
                    await userDAL.ExecuteAsync(sql2, db_user);
                }
                userDAL.Commit();
            }
            catch (Exception ex)
            {
                userDAL.Rollback();
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 动态条件用户删除(软删除)
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Expression<Func<User, bool>> exp)
        {

        }

        /// <summary>
        /// 单个真实删除(真实删除)
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        public async Task Delete_TrueAsync(UserDTO userDTO)
        {
            //数据库中对应的用户
            string sql = @"delete from t_users where Id=@Id";
            var db_user =await userDAL.GetEntityAsync(sql,userDTO);
        }

        /// <summary>
        /// 动态条件真实删除
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public async Task Delete_TrueAsync(Expression<Func<User, bool>> exp)
        {
         
        }

        /// <summary>
        /// 单个用户修改
        /// </summary>
        /// <param name="userDTO"></param>
        /// <param name="Names"></param>
        /// <returns></returns>
        public async Task UpdateAsync(UserDTO userDTO, string[] RoleIds)
        {
            try
            {
                //修改用户属性
                string sql1 = @"update t_users set Name=@Name,Password=@Password,Height=@Height,`Describe`=@Describe where Id=@Id";
                var result1 = await userDAL.ExecuteAsync(sql1, userDTO);
                //删除用户角色表数据，并添加新的
                string sql2 = @"delete from t_userrolerelations where User_Id=@Id";
                var result2 = await userRoleDAL.ExecuteAsync(sql2, userDTO);
                //新增新的用户角色关系
                string sql3 = @"insert into t_userrolerelations(User_Id,Role_Id) values(@User_Id,@Role_Id)";
                List<UserRole> list = new List<UserRole>();
                foreach (var RoleId in RoleIds)
                {
                    list.Add(new UserRole() { User_Id=userDTO.Id,Role_Id=RoleId});
                }
                var result3 = await userRoleDAL.ExecuteAsync(sql3, list.ToArray());
                userDAL.Commit();
            }
            catch (Exception ex)
            {
                userDAL.Rollback();
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 单个UserRoleDTO查找
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<UserRoleDTO> GetUpdateEntityAsync(string Id)
        {
            //数据库中对应的用户
            string sql1 = @"select * from t_users where Id=@Id";
            var db_user = await userDAL.GetEntityAsync(sql1,new { Id = Id });
            //用户对应的Role集合
            string sql2 = @"select role.* from t_users as user join t_userrolerelations as userrole on user.Id=userrole.User_Id join t_roles as role on userrole.Role_Id=role.Id where user.Id=@Id";
            var db_roles = await roleDAL.GetFilterAsync(sql2, new { Id = Id });
            //数据库中所有的角色集合
            string sql3=@"select * from t_roles";
            var db_roles_all = (await roleDAL.GetFilterAsync(sql3,null)).ToList().Select(e=>Mapper.Map<RoleDTO>(e)).ToList();
            //标记用户对应的角色
            foreach (var db_role in db_roles_all)
            {
                if (db_roles.Select(e => e.Id).Contains(db_role.Id))
                {
                    db_role.IsChoosed = true;
                }
                else
                {
                    db_role.IsChoosed = false;
                }
            }
            return new UserRoleDTO() { UserDTO=Mapper.Map<UserDTO>(db_user),RoleDTOs= db_roles_all };
        }

        /// <summary>
        /// 获取用户所拥有的权限
        /// </summary>
        /// <param name="Id">用户id</param>
        /// <returns></returns>
        public async Task<List<Module>> GetUserModules(string Id)
        {
            //用户拥有的总的模块
            var User_Modules = new List<Module>();
            //用户拥有的角色对应的模块
            string sql1 = @"select module from t_users as user join t_userrolerelations as userrole on user.Id=userrole.User_Id join t_roles as role on userrole.Role_Id=role.Id join t_rolemodulerelations as rolemodule on role.Id=rolemodule.Role_Id join t_modules as module on rolemodule.Module_Id=module.Id where user.Id=@Id";
            var result1 =await moduleDAL.GetFilterAsync(sql1,new { Id=Id});
            User_Modules.AddRange(result1);
            //用户直接拥有的模块
            string sql2 = @"select module from t_users as user join t_usermodulerelations as usermodule on user.Id=usermodule.User_Id join t_modules as module on usermodule.Module_Id=module.Id where user.Id=@Id";
            var result2= await moduleDAL.GetFilterAsync(sql2, new { Id = Id });
            User_Modules.AddRange(result2);
            return User_Modules.GroupBy(e=>e.Id).Select(e=>e.FirstOrDefault()).ToList();
        }

        /// <summary>
        /// 判断用户是否拥有该权限
        /// </summary>
        /// <param name="Id">用户id</param>
        /// <param name="ControllerName">控制器名字</param>
        /// <param name="ActionName">action名字</param>
        /// <returns></returns>
        public async Task<bool> GetPermissionFlagAsync(string Id, string ControllerName, string ActionName)
        {
            //用户拥有的权限
            var User_Modules =await GetUserModules(Id);
            //判断用户的模块是否拥有当前控制器名和action名对应的模块
            if (User_Modules.Where(e => e.ControllerName == ControllerName && e.ActionName == ActionName && e.IsDeleted == false).Any())
            {
                //用户拥有该权限
                return true;
            }
            else
            {
                //用户不拥有该权限
                return false;
            }
        }

        /// <summary>
        /// 获取用户对应的角色，模块名字
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<List<UserRoleModuleDTO>> GetUserRoleModuleAsync(string Id)
        {
            //数据库中对应的用户
            string sql1 = @"select * from t_users where Id=@Id";
            var db_user = await userDAL.GetEntityAsync(sql1,new { Id=Id});
            //用户对应的角色集合
            string sql2 = @"select role.* from t_users as user join t_userrolerelations as userrole on user.Id=userrole.User_Id join t_roles as role on userrole.Role_Id=role.Id where user.Id=@Id";
            var db_roles = await roleDAL.GetFilterAsync(sql2, new { Id = Id });
            //用户对应的角色名，模块名
            var UserRoleModuleDTOs = new List<UserRoleModuleDTO>();
            foreach (var db_role in db_roles)
            {
                //角色对应的模块名
                string sql3 = @"select module.* from t_roles as role join t_rolemodulerelations as rolemodule on role.Id=rolemodule.Role_Id join t_modules as module on rolemodule.Module_Id=module.Id where role.Id=@Id";
                var db_modules = (await moduleDAL.GetFilterAsync(sql3,new { Id=db_role.Id})).ToList();
                foreach (var db_module in db_modules)
                {
                    UserRoleModuleDTOs.Add(new UserRoleModuleDTO() { Id=Id,UserName= db_user.Name,Password= db_user.Password,RoleName=db_role.Name,ModuleName=db_module.ModuleName });
                }
            }
            return UserRoleModuleDTOs;
        }

        /// <summary>
        /// 获取所有isdeleted标志为false的user
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserDTO>> GetAllUserDTOsAsync()
        {
            string sql = @"select * from t_users where IsDeleted=false";
            var result = (await userDAL.GetFilterAsync(sql, null)).ToList();
            return result.Select(e => Mapper.Map<UserDTO>(e)).ToList();
        }

        /// <summary>
        /// 根据用户名和密码判断该用户是否存在
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public async Task<UserDTO> LoginAsync(string Name, string Password)
        {
            string sql = @"select * from t_users where IsDeleted=false and Name=@Name and Password=@Password";
            var result = await userDAL.GetEntityAsync(sql,new { Name=Name,Password=Password});
            if (result != null)
                return Mapper.Map<UserDTO>(result);
            return null;
        }

        /// <summary>
        /// 根据id删除user
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task DeleteByIdAsync(string Id)
        {
            string sql = @"delete from t_users where Id=@Id";
            await userDAL.ExecuteAsync(sql,new { Id=Id});
        }

        /// <summary>
        /// 根据动态条件查询单个user
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        public async Task<UserDTO> GetEntityAsync(Dictionary<string, object> dict)
        {
            string sql = "select * from t_users where 1=1";
            var result=(await userDAL.ByWhere(sql,dict)).FirstOrDefault();
            if (result != null)
                return Mapper.Map<UserDTO>(result);
            return null;
        }

        /// <summary>
        /// 根据动态条件查询user集合
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        public async Task<List<UserDTO>> GetFilterAsync(Dictionary<string, object> dict)
        {
            string sql = "select * from t_users where 1=1";
            var result = (await userDAL.ByWhere(sql, dict)).ToList();
            return result.Select(e => Mapper.Map<UserDTO>(e)).ToList();
        }

        /// <summary>
        /// 根据动态条件删除指定的group
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Dictionary<string, object> dict)
        {
            string sql = "delete from t_users where 1=1";
            await userDAL.ExecuteAsync(sql,dict);
        }
    }
}
