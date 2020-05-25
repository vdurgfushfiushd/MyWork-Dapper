using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyWork.Controllers
{
    public class GeneralController : Controller
    {
        // GET: General
        [HttpGet]
        public ActionResult Error()
        {
            return View();
        }

        [HttpGet]
        public ActionResult PermissionsWarning()
        {
            return View("PermissionsWarning", (object)"大兄弟，你没有权限进行该操作");
        }

        [HttpGet]
        public ActionResult LoginWarning()
        {
            return View("LoginWarning", (object)"大兄弟，你还没有登录,请登录在进行操作");
        }

        [HttpGet]
        public ActionResult ShowError(string Message)
        {
            return View(Message);
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}