using DTO;
using IBLL;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MyWork.Controllers
{
    public class NoteController : Controller
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
            var list = await noteBLL.GetFilterAsync(dict);
            return View(list);
        }

        [HttpGet]
        public async Task<JsonResult> GetAll()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("IsDeleted", false);
            var list = (await noteBLL.GetFilterAsync(dict));
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// 日志新增
        /// </summary>
        /// <param name="noteDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> Add(NoteDTO noteDTO)
        {
            try
            {
                noteDTO.User_Id = Session["UserId"].ToString();
                await noteBLL.AddAsync(noteDTO);
                return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "success", Message = "" });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "fail", Message = ex.Message });
            }
        }

        /// <summary>
        /// 日志删除
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Delete(string Id)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("Id", Id);
            dict.Add("IsDeleted", false);
            await noteBLL.DeleteAsync(dict);
            return Redirect("/Note/Index");
        }

        /// <summary>
        /// 日志修改页面
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Update(string Id)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("Id",Id);
            dict.Add("IsDeleted", false);
            var note = await noteBLL.GetEntityAsync(dict);
            return View(note);
        }

        /// <summary>
        /// 日志修改
        /// </summary>
        /// <param name="noteDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> Update(NoteDTO noteDTO)
        {
            try
            {
                await noteBLL.UpdateAsync(noteDTO);
                return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "success", Message = "" });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "fail", Message = ex.Message });
            }
        }

        /// <summary>
        /// 我的日志
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> MyNote()
        {
            var UserId=Session["UserId"].ToString();
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("User_Id",UserId);
            dict.Add("IsDeleted", false);
            var list = await noteBLL.GetFilterAsync(dict);
            return View(list);
        }
    }
}