using AutoMapper;
using DTO;
using IBLL;
using IDAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Module = Model.Module;

namespace BLL
{
    public class ModuleBLL : IModuleBLL
    {
        public INoteDAL noteDAL { get; set; }

        public IUserDAL userDAL { get; set; }

        public IRoleDAL roleDAL { get; set; }

        public IModuleDAL moduleDAL { get; set; }

        /// <summary>
        /// 单个模块新增
        /// </summary>
        /// <param name="moduleDTO"></param>
        /// <returns></returns>
        public async Task AddAsync(ModuleDTO moduleDTO)
        {
            //数据库中对应的模块
            string sql1 = @"select * from t_modules where Id=@Id";
            var db_module = await moduleDAL.GetEntityAsync(sql1,new { Id= moduleDTO.Id });
            //如果数据库中没有改数据，则新增
            if (db_module == null)
            {
                //要新增的数据
                var module = Mapper.Map<Module>(moduleDTO);
                //设置属性
                module.Id = Guid.NewGuid().ToString("n");
                module.IsDeleted = false;
                module.CreateTime = DateTime.Now;
                string sql2 = @"insert into t_modules(Id,ModuleId,ModuleName,`Describe`,ControllerName,ActionName,CreateTime,IsDeleted) values(@Id,@ModuleId,@ModuleName,@Describe,@ControllerName,@ActionName,@CreateTime,@IsDeleted)";
                //模块新增
                await moduleDAL.ExecuteAsync(sql2, module);
            }
        }

        /// <summary>
        /// 单个模块删除(软删除)
        /// </summary>
        /// <param name="moduleDTO"></param>
        /// <returns></returns>
        public async Task DeleteAsync(ModuleDTO moduleDTO)
        {
            //数据库中对应的模块
            string sql1 = @"select * from t_modules where Id=@Id";
            var db_module = await moduleDAL.GetEntityAsync(sql1,new { Id=moduleDTO.Id});
            //设置模块的删除标志为true
            if (db_module != null)
            {
                string sql2 = @"update t_modules set IsDeleted=true where Id=@Id";
                await moduleDAL.ExecuteAsync(sql2, db_module);
            }
        }

        /// <summary>
        /// 单个模块删除(真实删除)
        /// </summary>
        /// <param name="moduleDTO"></param>
        /// <returns></returns>
        public async Task Delete_TrueAsync(ModuleDTO moduleDTO)
        {
            //修改数据库中对应的模块
            string sql1 = @"delete from t_modules where Id=@Id";
            var db_module = await moduleDAL.ExecuteAsync(sql1, moduleDTO);
            //删除模块和角色关系数据
            string sql2 = @"delete from t_rolemodulerelations where Id=@Id";
            await moduleDAL.ExecuteAsync(sql2, new { Id = moduleDTO.Id });
            //删除模块和用户关系数据
            string sql3 = @"delete from t_usermodulerelations where Id=@Id";
            await moduleDAL.ExecuteAsync(sql3, new { Id = moduleDTO.Id });
        }

        /// <summary>
        /// 模块修改
        /// </summary>
        /// <param name="moduleDTO"></param>
        /// <returns></returns>
        public async Task UpdateAsync(ModuleDTO moduleDTO, string[] actionNames)
        {
            //删除旧的所有的ModuleId为module.ModuleId的模型类的集合
            string sql1 = @"delete from t_modules where ModuleId=@ModuleId";
            await moduleDAL.ExecuteAsync(sql1, new { ModuleId = moduleDTO.ModuleId });
            //新增新的module
            List<Module> list = new List<Module>();
            foreach (var actionName in actionNames)
            {
                Module _module = new Module();
                _module.Id = Guid.NewGuid().ToString("n");
                _module.CreateTime = DateTime.Now;
                _module.IsDeleted = false;
                _module.ModuleId = moduleDTO.ModuleId;
                _module.ActionName = actionName;
                _module.ControllerName = moduleDTO.ControllerName;
                _module.ModuleName = moduleDTO.ModuleName;
                _module.Describe = moduleDTO.Describe;
                list.Add(_module);
            }
            string sql2 = @"insert into t_modules(Id,ModuleId,ModuleName,`Describe`,ControllerName,ActionName,CreateTime,IsDeleted) values(@Id,@ModuleId,@ModuleName,@Describe,@ControllerName,@ActionName,@CreateTime,@IsDeleted)";
            await moduleDAL.ExecuteAsync(sql2,list);
        }

        /// <summary>
        /// 获取当前项目的所有的control
        /// </summary>
        /// <returns></returns>
        public List<string> GetControls()
        {
            List<string> controls = new List<string>();
            var asm = Assembly.Load("MyWork");
            List<Type> typeList = new List<Type>();
            var types = asm.GetTypes();
            foreach (Type type in types)
            {
                //去掉非自己创建的控制器
                //if (type.Name.StartsWith("<>")) continue;
                //else if (type.Name.StartsWith("MvcApplication")) continue;
                //else if (type.Name.StartsWith("RouteConfig")) continue;
                //else typeList.Add(type);
                if (type.Name.EndsWith("Controller"))
                {
                    if (type.Name.Equals("GeneralController"))
                    {
                        continue;
                    }
                    else
                    {
                        typeList.Add(type);
                    }
                }

            }
            typeList.Sort(delegate (Type type1, Type type2) { return type1.FullName.CompareTo(type2.FullName); });
            controls = typeList.Select(e => e.Name).ToList();
            return controls;
        }

        /// <summary>
        /// 获取当前项目中的指定的控制器所有的action
        /// </summary>
        /// <param name="ControlName">控制器名字</param>
        /// <returns></returns>
        public List<string> GetActions(string ControlName)
        {
            List<string> actions = new List<string>();
            var asm = Assembly.Load("MyWork");
            List<Type> typeList = new List<Type>();
            var types = asm.GetTypes().Where(e => e.Name == (ControlName));
            typeList.AddRange(types);
            typeList.Sort(delegate (Type type1, Type type2) { return type1.FullName.CompareTo(type2.FullName); });
            foreach (Type type in typeList)
            {
                System.Reflection.MemberInfo[] members = type.FindMembers(System.Reflection.MemberTypes.Method,
                System.Reflection.BindingFlags.Public |
                System.Reflection.BindingFlags.Static |
                System.Reflection.BindingFlags.NonPublic |        //【位屏蔽】
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.DeclaredOnly,
                Type.FilterName, "*");
                foreach (var m in members)
                {
                    if (m.DeclaringType.Attributes.HasFlag(System.Reflection.TypeAttributes.Public) != true)
                        continue;
                    string controller = type.Name.Replace("Controller", "");
                    string action = m.Name;
                    if (actions.Contains(action))
                    {
                        continue;
                    }
                    else
                    {
                        if (action.EndsWith("BLL"))
                        {
                            continue;
                        }
                        else
                        {
                            actions.Add(action);
                        }
                    }
                }
            }
            return actions;
        }

        /// <summary>
        /// 新增模块（单个）
        /// </summary>
        /// <param name="module"></param>
        /// <param name="ActionNames"></param>
        public async Task AddAsync(ModuleDTO moduleDTO, string[] ActionNames)
        {
            //数据库中该控制器名称对应的模块名的模块集合
            string sql1 = @"select * from t_modules where ControllerName=@ControllerName and IsDeleted == false";
            var db_modules = await moduleDAL.GetFilterAsync(sql1,new { ControllerName = moduleDTO.ControllerName });
            //如果数据库中有该控制器对应的模块，则抛出警告，否则新增
            if (db_modules.Any())
            {
                throw new Exception("数据库中已经有该控制器对应的模块，请不要重复添加");
            }
            else
            {
                List<Module> list = new List<Module>();
                moduleDTO.ModuleId = Guid.NewGuid().ToString("n");
                foreach (var actionName in ActionNames)
                {
                    Module _module = new Module();
                    _module.Id = Guid.NewGuid().ToString("n");
                    _module.CreateTime = DateTime.Now;
                    _module.IsDeleted = false;
                    _module.ModuleId = moduleDTO.ModuleId;
                    _module.ActionName = actionName;
                    _module.ControllerName = moduleDTO.ControllerName;
                    _module.ModuleName = moduleDTO.ModuleName;
                    _module.Describe = moduleDTO.Describe;
                    list.Add(_module);
                }
                string sql2 = @"insert into t_modules(Id,ModuleId,ModuleName,`Describe`,ControllerName,ActionName,CreateTime,IsDeleted) values(@Id,@ModuleId,@ModuleName,@Describe,@ControllerName,@ActionName,@CreateTime,@IsDeleted)";
                await moduleDAL.ExecuteAsync(sql2, list);
            }
        }

        /// <summary>
        /// 获取要修改的module
        /// </summary>
        /// <param name="ModuleId"></param>
        /// <returns></returns>
        public async Task<ModuleViewDTO> GetUpdateModulesAsync(string ModuleId)
        {
            //要修改的模块对象
            string sql1 = @"select * from t_modules where ModuleId=@ModuleId";
            var module = (await moduleDAL.GetFilterAsync(sql1,new { ModuleId = ModuleId })).FirstOrDefault();
            //module表中有的action
            var actions = (await moduleDAL.GetFilterAsync(sql1, new { ModuleId = ModuleId })).Select(e => e.ActionName).ToList();
            //控制器对应的所有的action
            var allActions = GetActions(module.ControllerName);
            List<ModuleDTO> list = new List<ModuleDTO>();
            foreach (var action in allActions)
            {
                if (actions.Contains(action))
                {
                    list.Add(new ModuleDTO() { ActionName = action, IsChoosed = true });
                }
                else
                {
                    list.Add(new ModuleDTO() { ActionName = action, IsChoosed = false });
                }
            }
            return new ModuleViewDTO() { ModuleDTO = Mapper.Map<ModuleDTO>(module), ModuleDTOs = list };
        }

        /// <summary>
        /// 获取所有isdeleted标志为false的module
        /// </summary>
        /// <returns></returns>
        public async Task<List<ModuleDTO>> GetAllModuleDTOsAsync()
        {
            string sql = @"select * from t_modules where IsDeleted=false";
            var result = (await moduleDAL.GetFilterAsync(sql, null)).ToList();
            return result.Select(e => Mapper.Map<ModuleDTO>(e)).ToList();
        }

        /// <summary>
        /// 根据动态条件查询单个module
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        public async Task<ModuleDTO> GetEntityAsync(Dictionary<string, object> dict)
        {
            string sql = "select * from t_modules where 1=1";
            var result = (await moduleDAL.ByWhere(sql, dict)).FirstOrDefault();
            if (result != null)
                return Mapper.Map<ModuleDTO>(result);
            return null;
        }

        /// <summary>
        /// 根据动态条件查询module集合
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        public async Task<List<ModuleDTO>> GetFilterAsync(Dictionary<string, object> dict)
        {
            try
            {
                string sql = "select * from t_modules where 1=1";
                var result = (await moduleDAL.ByWhere(sql, dict)).ToList();
                return result.Select(e => Mapper.Map<ModuleDTO>(e)).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 根据动态条件删除指定的module
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Dictionary<string, object> dict)
        {
            string sql = "delete from t_modules where 1=1";
            await moduleDAL.ExecuteAsync(sql, dict);
        }
    }
}
