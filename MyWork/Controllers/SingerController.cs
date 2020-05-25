using DTO;
using IBLL;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MyWork.Controllers
{
    public class SingerController : Controller
    {
        public IUserBLL userBLL { get; set; }

        public IRoleBLL roleBLL { get; set; }

        public IModuleBLL moduleBLL { get; set; }

        public INoteBLL noteBLL { get; set; }

        public IMusicBLL musicBLL { get; set; }

        public ISingerBLL singerBLL { get; set; }

        // GET: Singer
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            SingerListDTO singerListDTO = new SingerListDTO();
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("IsDeleted",false);
            var list = await singerBLL.GetFilterAsync(dict);
            singerListDTO.Countrys = list.Select(e => e.Country).Distinct().ToList();
            singerListDTO.Styles = list.Select(e => e.Style).Distinct().ToList();
            singerListDTO.singerDTOs = null;
            return PartialView(singerListDTO);
        }

        [HttpGet]
        public async Task<ActionResult> SingerList()
        {
            SingerListDTO singerListDTO = new SingerListDTO();
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("IsDeleted", false);
            var list =await singerBLL.GetFilterAsync(dict);
            singerListDTO.Countrys = list.Select(e=>e.Country).Distinct().ToList();
            singerListDTO.Styles= list.Select(e => e.Style).Distinct().ToList();
            singerListDTO.singerDTOs = null;
            return PartialView(singerListDTO);
        }

        [HttpGet]
        public async Task<JsonResult> SingerLists(string Country, string FirstLetter, string Sex, string Style)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("IsDeleted", false);
            var list = (await singerBLL.GetFilterAsync(dict)).ToList();
            if (Country != "全部")
            {
                list = list.Where(e=>e.Country==Country).ToList();
            }
            if (FirstLetter != "全部")
            {
                list = list.Where(e => e.FirstLetter == FirstLetter).ToList();
            }
            if (Sex != "全部")
            {
                list = list.Where(e => e.Sex == Sex).ToList();
            }
            if (Style != "全部")
            {
                list = list.Where(e => e.Style == Style).ToList();
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult SingerMusics(string Name)
        {
            return View("SingerMusics", (object)Name);
        }

        [HttpGet]
        public async Task<JsonResult> ShowSingerMusic(string Name)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("IsDeleted", false);
            var list = await musicBLL.GetFilterAsync(dict);
            return Json(list,JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// 歌手新增
        /// </summary>
        /// <param name="singerDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> Add(SingerDTO singerDTO)
        {
            try
            {
                //singerDTO.FirstLetter = StringHelper.ChineseToEnglish(singerDTO.Name);
                await singerBLL.AddAsync(singerDTO);
                return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "success" });
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
            dict.Add("Id", Id);
            dict.Add("IsDeleted", false);
            var singerDTO = await singerBLL.GetEntityAsync(dict);
            return View(singerDTO);
        }

        [HttpPost]
        public async Task<string> Update(SingerDTO singerDTO)
        {
            try
            {
                await singerBLL.UpdateAsync(singerDTO);
                return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "success", Message = "" });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "fail", Message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<ActionResult> Delete(string Id)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("Id", Id);
            dict.Add("IsDeleted", false);
            await singerBLL.DeleteAsync(dict);
            return View("ok");
        }

    }
}