using AutoMapper;
using DTO;
using IBLL;
using IDAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BLL
{
    public class RoleBLL:IRoleBLL
    {
        public INoteDAL noteDAL { get; set; }

        public IUserDAL userDAL { get; set; }

        public IRoleDAL roleDAL { get; set; }

        public IRoleModuleDAL roleModuleDAL { get; set; }

        public IModuleDAL moduleDAL { get; set; }

        /// <summary>
        /// 单个角色新增(异步)
        /// </summary>
        /// <param name="roleDTO"></param>
        /// <returns></returns>
        public async Task AddAsync(RoleDTO roleDTO)
        {
            //数据库中的角色数据
            string sql1 = @"select * from t_roles where Id=@Id";
            var db_role = await roleDAL.GetEntityAsync(sql1,new { Id=roleDTO.Id});
            //数据库中没有改角色，则新增
            if (db_role == null)
            {
                //要新增的数据
                var role = Mapper.Map<Role>(roleDTO);
                //属性修改
                role.Id = Guid.NewGuid().ToString("n");
                role.IsDeleted = false;
                role.CreateTime = DateTime.Now;
                //数据新增
                string sql2 = @"insert into t_roles(Id,Name,CreateTime,IsDeleted,`Describe`) values(@Id,@Name,@CreateTime,@IsDeleted,@Describe)";
                await roleDAL.ExecuteAsync(sql2,role);
            }
        }

        /// <summary>
        /// 单个删除角色(软删除)
        /// </summary>
        /// <param name="roleDTO"></param>
        /// <returns></returns>
        public async Task DeleteAsync(RoleDTO roleDTO)
        {
            //数据库中的角色数据
            string sql1 = @"select * from t_roles where Id=@Id";
            var db_role = await roleDAL.GetEntityAsync(sql1,new { Id=roleDTO.Id});
            //如果数据库中有该角色，设置其删除标记为true
            if (db_role != null)
            {
                string sql2 = @"update t_roles set IsDeleted=true where Id=@Id";
                await roleDAL.ExecuteAsync(sql2,db_role);
            }
        }

        /// <summary>
        /// 单个角色删除(真实删除)
        /// </summary>
        /// <param name="roleDTO"></param>
        /// <returns></returns>
        public async Task Delete_TrueAsync(RoleDTO roleDTO)
        {
            //数据库中对应的角色
            string sql1 = @"select * from t_roles where Id=@Id";
            var db_role = await roleDAL.GetEntityAsync(sql1,new { Id= roleDTO.Id });
            //删除角色用户关系表中的数据
            string sql2 = @"delete from t_userrolerelations where User_Id=@Id";
            await roleDAL.ExecuteAsync(sql2,roleDTO);
            //删除角色模块关系表中的数据
            string sql3 = @"delete from t_usermodulerelations where User_Id=@Id";
            await roleDAL.ExecuteAsync(sql3, roleDTO);
        }

        /// <summary>
        /// 修改角色数据
        /// </summary>
        /// <param name="roleDTO"></param>
        /// <param name="DescriptionIds"></param>
        /// <returns></returns>
        public async Task UpdateAsync(RoleDTO roleDTO,string[] ModuleIds)
        {
            try
            {
                //修改role的值
                string sql1 = @"update t_roles set Name=@Name,`Describe`=@Describe where Id=@Id";
                var db_role = await roleDAL.GetEntityAsync(sql1, roleDTO);
                //删除role-module关系表中的数据
                string sql2 = @"delete from t_rolemodulerelations where Role_Id=@Id";
                await roleDAL.ExecuteAsync(sql2, roleDTO);
                //新增role-module关系表中的数据
                string sql3 = @"insert into t_rolemodulerelations(Role_Id,Module_Id) values(@Role_Id,@Module_Id)";
                List<RoleModule> list = new List<RoleModule>();
                foreach (var ModuleId in ModuleIds)
                {
                    list.Add(new RoleModule() { Role_Id = roleDTO.Id, Module_Id = ModuleId });
                }
                await roleModuleDAL.ExecuteAsync(sql3, list);
                roleModuleDAL.Commit();
            }
            catch (Exception ex)
            {
                 roleModuleDAL.Rollback();
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 获取要修改的角色及其模型集合
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<RoleModuleDTO> GetUpdateEntityAsync(string Id)
        {
            //数据库中的对应的角色对象修改
            string sql1 = @"select * from t_roles where Id=@Id";
            var db_role = await roleDAL.GetEntityAsync(sql1,new { Id=Id});
            //角色对应的module集合
            string sql2 = @"select module.* from t_roles as role join t_rolemodulerelations as rolemodule on role.Id=rolemodule.Role_Id join t_modules as module on rolemodule.Module_Id=module.Id";
            var db_roles = await roleDAL.GetFilterAsync(sql2,new { Id=Id});
            //数据库当中所有的module集合
            string sql3 = @"select * from t_modules";
            var db_roles_all =(await moduleDAL.GetFilterAsync(sql3,null)).ToList().Select(e => Mapper.Map<ModuleDTO>(e)).ToList();
            //标记角色对应的模块
            foreach (var moduleDTO in db_roles_all)
            {
                //若角色模块关系表中有该模块，则标记该模块
                if (db_roles.Select(e => e.Id).Contains(moduleDTO.Id))
                {
                    moduleDTO.IsChoosed = true;
                }
                else
                {
                    moduleDTO.IsChoosed = false;
                }
            }
            return new RoleModuleDTO() { RoleDTO=Mapper.Map<RoleDTO>(db_role), ModuleDTOs = db_roles_all }; 
        }

        /// <summary>
        /// 获取指定角色及其模块
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<RoleModuleDTO> GetRoleModuleAsync(string Id)
        {
            //获取指定的角色
            string sql1 = @"select * from t_roles where Id=@Id";
            var db_role = await roleDAL.GetEntityAsync(sql1,new { Id=Id});
            //获取角色对应的模块
            string sql2 = @"select module.* from t_roles as role join t_rolemodulerelations as rolemodule on role.Id=rolemodule.Role_Id join t_modules as module on rolemodule.ModuleId=module.Id";
            var db_modules =await moduleDAL.GetFilterAsync(sql2,new { Id=Id});
            //指定角色及其对应的模块
            return new RoleModuleDTO() { RoleDTO=Mapper.Map<RoleDTO>(db_role),ModuleDTOs=db_modules.Select(e=>Mapper.Map<ModuleDTO>(e)).ToList() };
        }

        /// <summary>
        /// 获取所有isdeleted标志为false的role
        /// </summary>
        /// <returns></returns>
        public async Task<List<RoleDTO>> GetAllRoleDTOsAsync()
        {
            string sql = @"select * from t_roles where IsDeleted=false";
            var result = (await roleDAL.GetFilterAsync(sql, null)).ToList();
            return result.Select(e => Mapper.Map<RoleDTO>(e)).ToList();
        }

        /// <summary>
        /// 根据动态条件查询单个role
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        public async Task<RoleDTO> GetEntityAsync(Dictionary<string, object> dict)
        {
            try
            {
                string sql = "select * from t_roles where 1=1";
                var result = (await roleDAL.ByWhere(sql, dict)).FirstOrDefault();
                if (result != null)
                    return Mapper.Map<RoleDTO>(result);
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message); ;
            }
        }

        /// <summary>
        /// 根据动态条件查询singer集合
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        public async Task<List<RoleDTO>> GetFilterAsync(Dictionary<string, object> dict)
        {
            try
            {
                string sql = "select * from t_roles where 1=1";
                var result = (await roleDAL.ByWhere(sql, dict)).ToList();
                if (result.Any())
                  return result.Select(e => Mapper.Map<RoleDTO>(e)).ToList();
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 根据动态条件删除指定的role
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Dictionary<string, object> dict)
        {
            try
            {
                string sql = "delete from t_roles where 1=1";
                await roleDAL.ExecuteAsync(sql, dict);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
