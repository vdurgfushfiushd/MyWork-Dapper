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
    public class NoteManageController : Controller
    {
        public IUserBLL userBLL { get; set; }

        /// <summary>
        /// 程序首页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 用户登录页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> Login(UserDTO userDTO)
        {
            try
            {
                var userdto = await userBLL.LoginAsync(userDTO.Name,userDTO.Password);
                if (userdto == null)
                {
                    return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "fail",Message="没有此人，请检查用户名和密码" });
                }
                else
                {
                    Session["UserName"] = userdto.Name;
                    Session["UserId"] = userdto.Id;
                    return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "success" });
                }
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "fail", Message = ex.Message });
            }
        }
    }
}