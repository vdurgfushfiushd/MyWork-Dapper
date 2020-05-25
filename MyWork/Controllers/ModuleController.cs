using DTO;
using IBLL;
using Model;
using MyWork.App_Start.Filter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MyWork.Controllers
{
    public class ModuleController : Controller
    {
        public IUserBLL userBLL { get; set; }

        public IRoleBLL roleBLL { get; set; }

        public IModuleBLL moduleBLL { get; set; }
  
        public INoteBLL noteBLL { get; set; } 

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return View();
        }

        /// <summary>
        /// 获取指定控制器的action集合
        /// </summary>
        /// <param name="ControlName"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetActions(string ControlName)
        {
            var list = moduleBLL.GetActions(ControlName);
            var result = Json(list, JsonRequestBehavior.AllowGet);
            return result;
        }

        [HttpGet]
        public async Task<JsonResult> GetAll()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("IsDeleted", false);
            dict.Add("IsDeleted", false);
            var list = (await moduleBLL.GetFilterAsync(dict)).GroupBy(e => e.ModuleName).Select(e => e.FirstOrDefault());
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Add()
        {
            var controls = moduleBLL.GetControls();
            return View(controls);
        }

        /// <summary>
        /// 模块新增
        /// </summary>
        /// <param name="moduleDTO"></param>
        /// <param name="actionNames"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> Add(ModuleDTO moduleDTO,string[] actionNames)
        {
            try
            {
                await moduleBLL.AddAsync(moduleDTO, actionNames);
                return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "success", Message = "" });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "fail", Message = ex.Message });
            }
        }

        /// <summary>
        /// 模块删除
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Delete(string ModuleId)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("ModuleId", ModuleId);
            dict.Add("IsDeleted", false);
            await moduleBLL.DeleteAsync(dict);
            return Redirect("/Module/Index");
        }

        /// <summary>
        /// 模块修改页面
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Update(string ModuleId)
        {
            var ModuleViewDTO = await moduleBLL.GetUpdateModulesAsync(ModuleId);
            return View(ModuleViewDTO);
        }

        /// <summary>
        /// 模块修改
        /// </summary>
        /// <param name="moduleDTO"></param>
        /// <param name="actionNames"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> Update(ModuleDTO moduleDTO,string[] actionNames)
        {
            try
            {
                await moduleBLL.UpdateAsync(moduleDTO, actionNames);
                return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "success", Message = "" });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "fail", Message = ex.Message });
            }
        }

        /// <summary>
        /// 查看指定模块的详细内容
        /// </summary> 
        /// <param name="ModuleId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowModule(string ModuleId)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("ModuleId",ModuleId);
            dict.Add("IsDeleted",false);
            var list = await moduleBLL.GetFilterAsync(dict);
            return View(list);
        }
    }
}