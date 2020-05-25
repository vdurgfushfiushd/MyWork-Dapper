using DTO;
using IBLL;
using MongoDB.Bson;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MyWork.Controllers
{
    public class CommentsController : Controller
    {
        public IUserBLL userBLL { get; set; }

        public IRoleBLL roleBLL { get; set; }

        public IModuleBLL moduleBLL { get; set; }

        public INoteBLL noteBLL { get; set; }

        public ICommentsBLL commentsBLL { get; set; }
        
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var list = await commentsBLL.GetFilterAsync(e=>e.IsDeleted==false);
            return View(list);
        }

        [HttpGet]
        public async Task<JsonResult> GetAll(int page)
        {
            var list = (await commentsBLL.GetFilterAsync(e => e.IsDeleted==false));
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(string Id)
        {
            var id = new ObjectId(Id);
            await commentsBLL.DeleteAsync(e=>e.Id==id&&e.IsDeleted==false);
            return Redirect("/Comments/Index");
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<string> Add(CommentsDTO commentsDTO)
        {
            try
            {
                await commentsBLL.AddAsync(commentsDTO);
                return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "success" });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "fail",Message=ex.Message });
            }
        }

        [HttpGet]
        public async Task<ActionResult> Update(string Id)
        {
            var id = new ObjectId(Id);
            var entity= await commentsBLL.GetEntityAsync(e=>e.Id==id);
            return View(entity);
        }

        [HttpPost]
        public async Task<ActionResult> Update(string Id,CommentsDTO commentsDTO)
        {
            commentsDTO.Id = new ObjectId(Id);
            await commentsBLL.UpdateAsync(commentsDTO);
            return Redirect("/Comments/Index");
        }
    }
}