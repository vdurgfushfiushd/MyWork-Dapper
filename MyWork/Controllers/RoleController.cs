using DTO;
using IBLL;
using Model;
using MyWork.App_Start.Filter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MyWork.Controllers
{
    public class RoleController : Controller
    {
        
        public IUserBLL userBLL { get; set; }

        public IRoleBLL roleBLL { get; set; }

        public IModuleBLL moduleBLL { get; set; }

        public INoteBLL noteBLL { get; set; } 

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("IsDeleted", false);
            var list = await roleBLL.GetFilterAsync(dict);
            return View(list);
        }

        [HttpGet]
        public async Task<JsonResult> GetAll()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("IsDeleted",false);
            var list = (await roleBLL.GetFilterAsync(dict));
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 角色新增界面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// 角色新增
        /// </summary>
        /// <param name="roleDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> Add(RoleDTO roleDTO)
        {
            try
            {
                await roleBLL.AddAsync(roleDTO);
                return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "success", Message = "" });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "fail", Message = ex.Message });
            }
        }

        /// <summary>
        /// 角色删除
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Delete(string Id)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("Id", Id);
            dict.Add("IsDeleted", false);
            await roleBLL.DeleteAsync(dict);
            return Redirect("/Role/Index");
        }

        /// <summary>
        /// 角色修改界面
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Update(string Id)
        {
            var rolemoduleDTO = await roleBLL.GetUpdateEntityAsync(Id);
            return View(rolemoduleDTO);
        }

        /// <summary>
        /// 角色修改
        /// </summary>
        /// <param name="roleDTO"></param>
        /// <param name="Module_Ids"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> Update(RoleDTO roleDTO,string[] Module_Ids)
        {
            try
            {
                await roleBLL.UpdateAsync(roleDTO, Module_Ids);
                return JsonConvert.SerializeObject(new ResponseClass() { State = "success" });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new ResponseClass() { State = "fail",Message=ex.Message });
            }
        }

        /// <summary>
        /// 根据角色id查询关联角色的权限
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowRoleModule(string Id)
        {
            var list = await roleBLL.GetRoleModuleAsync(Id);
            return View(list);
        }
    }
}