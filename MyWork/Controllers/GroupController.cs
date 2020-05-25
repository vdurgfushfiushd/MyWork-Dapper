using DTO;
using IBLL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MyWork.Controllers
{
    public class GroupController : Controller
    {
        public IGroupBLL groupBLL { get; set; }

        /// <summary>
        /// 群组首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetAll()
        {
            var list = (await groupBLL.GetAllGroupDTOsAsync());
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> GetData(string GroupName)
        {
            if (!string.IsNullOrEmpty(GroupName))
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict.Add("GroupName", GroupName);
                dict.Add("IsDeleted", false);
                var list = (await groupBLL.GetFilterAsync(dict));
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var list = (await groupBLL.GetAllGroupDTOsAsync());
                return Json(list, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 群组新增页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// 群组新增
        /// </summary>
        /// <param name="groupDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> Add(GroupDTO groupDTO)
        {
            try
            {
                await groupBLL.AddAsync(groupDTO);
                return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "success", Message = "" });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "fail", Message = ex.Message });
            }
        }

        /// <summary>
        /// 群组删除
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Delete(string Id)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("Id", Id);
            dict.Add("IsDeleted", false);
            await groupBLL.DeleteAsync(dict);
            return Redirect("/Group/Index");
        }

        /// <summary>
        /// 群组修改页面
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Update(string Id)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("Id", Id);
            dict.Add("IsDeleted", false);
            var GroupDTO = await groupBLL.GetEntityAsync(dict);
            return View(GroupDTO);
        }

        /// <summary>
        /// 群组修改
        /// </summary>
        /// <param name="groupDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> Update(GroupDTO groupDTO)
        {
            try
            {
                await groupBLL.Update(groupDTO);
                return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "success", Message = "" });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "fail", Message = ex.Message });
            }
        }
    }
}