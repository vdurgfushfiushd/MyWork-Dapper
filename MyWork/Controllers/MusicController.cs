using DTO;
using IBLL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MyWork.Controllers
{
    public class MusicController : Controller
    {
        public IUserBLL userBLL { get; set; }

        public IRoleBLL roleBLL { get; set; }

        public IModuleBLL moduleBLL { get; set; }

        public INoteBLL noteBLL { get; set; }

        public IMusicBLL musicBLL { get; set; }

        public ISingerBLL singerBLL { get; set; }

        // GET: Music
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            MusicListDTO musicListDTO = new MusicListDTO();
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("IsDeleted", false);
            var list = await musicBLL.GetFilterAsync(dict);
            musicListDTO.Styles = list.Select(e => e.Style).Distinct().ToList();
            musicListDTO.Themes = list.Select(e => e.Theme).Distinct().ToList();
            musicListDTO.Languages = list.Select(e => e.Language).Distinct().ToList();
            return View(musicListDTO);
        }

        [HttpGet]
        public async Task<JsonResult> MusicLists(string Style,string Theme,string Language)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("IsDeleted", false);
            var list=await musicBLL.GetFilterAsync(dict);
            if (Style != "全部"&&!string.IsNullOrEmpty(Style))
            {
                list = list.Where(e => e.Style == Style).ToList();
            }
            if (Theme != "全部" && !string.IsNullOrEmpty(Theme))
            {
                list = list.Where(e => e.Theme == Theme).ToList();
            }
            if (Language != "全部" && !string.IsNullOrEmpty(Language))
            {
                list = list.Where(e => e.Language == Language).ToList();
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> GetData(string Name, string Style, string Theme, string Language)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("IsDeleted", false);
            var list = await musicBLL.GetFilterAsync(dict);
            if (!string.IsNullOrEmpty(Style))
            {
                list = list.Where(e => e.Style == Style).ToList();
            }
            if (Style != "全部" && !string.IsNullOrEmpty(Style))
            {
                list = list.Where(e => e.Style == Style).ToList();
            }
            if (Theme != "全部" && !string.IsNullOrEmpty(Theme))
            {
                list = list.Where(e => e.Theme == Theme).ToList();
            }
            if (Language != "全部" && !string.IsNullOrEmpty(Language))
            {
                list = list.Where(e => e.Language == Language).ToList();
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> GetAll()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("IsDeleted", false);
            var list = (await musicBLL.GetFilterAsync(dict));
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// music新增
        /// </summary>
        /// <param name="musicDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> Add(MusicDTO musicDTO)
        {
            try
            {
                await musicBLL.AddAsync(musicDTO);
                return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "success", Message = "" });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "fail", Message = ex.Message });
            } 
        }

        //music删除
        [HttpGet]
        public async Task<ActionResult> Delete(string Id)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("Id", Id);
            dict.Add("IsDeleted", false);
            await musicBLL.DeleteAsync(dict);
            return View("/Music/Index");
        }

        [HttpPost]
        public async Task<string> UpdateAsync(MusicDTO musicDTO)
        {
            try
            {
                await musicBLL.UpdateAsync(musicDTO);
                return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "success", Message = "" });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "fail", Message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<ActionResult> Update(string Id)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("Id",Id);
            dict.Add("IsDeleted", false);
            var musicDTO = await musicBLL.GetEntityAsync(dict);
            return View(musicDTO);
        }

        //music上传
        [HttpPost]
        public ActionResult Upload()
        {
            try
            {
                HttpFileCollectionBase files = Request.Files;
                HttpPostedFileBase file = files[0];
                //获取文件名后缀
                string extName = Path.GetExtension(file.FileName).ToLower();
                //获取保存目录的物理路径
                if (System.IO.Directory.Exists(Server.MapPath("/Musics/")) == false)//如果不存在就创建images文件夹
                {
                    System.IO.Directory.CreateDirectory(Server.MapPath("/Musics/"));
                }
                string path = Server.MapPath("/Musics/"); //path为某个文件夹的绝对路径，不要直接保存到数据库
                //string path = "F:\\TgeoSmart\\Image\\";
                //生成新文件的名称，guid保证某一时刻内图片名唯一（文件不会被覆盖）
                string fileNewName = Guid.NewGuid().ToString();
                //string ImageUrl = path + fileNewName + extName;
                string MusicUrl = path + file.FileName;
                //SaveAs将文件保存到指定文件夹中
                file.SaveAs(MusicUrl);
                //此路径为相对路径，只有把相对路径保存到数据库中图片才能正确显示（不加~为相对路径）
                //string url = "\\Musics\\" + fileNewName + extName;
                string url = "\\Musics\\" + file.FileName;
                return Json(new
                {
                    Result = true,
                    Data = url
                });
            }
            catch (Exception exception)
            {
                return Json(new
                {
                    Result = false,
                    exception.Message
                });
            }
        }

        [HttpGet]
        public async Task<ActionResult> SingleMusic(string Id)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("Id",Id);
            dict.Add("IsDeleted", false);
            var musicDTO = await musicBLL.GetEntityAsync(dict);
            return View(musicDTO);
        }

        [HttpGet]
        public ActionResult Manage()
        {
            return View();
        }
    }
}