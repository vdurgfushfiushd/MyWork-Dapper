
using DTO;
using IBLL;
using Model;
using MyWork.App_Start.Filter;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MyWork.Controllers
{
    public class UserController : Controller
    {
        public IUserBLL userBLL { get; set; }

        public IRoleBLL roleBLL { get; set; }

        public IModuleBLL moduleBLL { get; set; }

        public INoteBLL noteBLL { get; set; } 

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetAll()
        {
            var list = await userBLL.GetAllUserDTOsAsync();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 用户新增界面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// 用户新增
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> Add(UserDTO userDTO)
        { 
            try
            {
                userDTO.Id = Guid.NewGuid().ToString();
                userDTO.CreateTime = DateTime.Now;
                userDTO.IsDeleted = false;
                await userBLL.AddAsync(userDTO);
                return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "success", Message = "" });
            }
            catch (Exception ex) 
            {
                return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "fail", Message = ex.Message });
            }
        }

        /// <summary>
        /// 根据用户id删除指定的用户信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Delete(string Id)
        {
            await userBLL.DeleteByIdAsync(Id);
            return Redirect("/User/Index");
        }

        /// <summary>
        /// 根据用户id获取要修改的用户
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Update(string Id)
        {
            var userRoleDTO = await userBLL.GetUpdateEntityAsync(Id);
            return View(userRoleDTO);
        }

        /// <summary>
        /// 修改用户信息和关联表的数据
        /// </summary>
        /// <param name="userDTO"></param>
        /// <param name="Role_Ids"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> Update(UserDTO userDTO,string[] Role_Ids)
        {
            try
            {
                await userBLL.UpdateAsync(userDTO, Role_Ids);
                return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "success" });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "fail",Message= ex.Message });
            }
        }

        /// <summary>
        /// 根据用户id查询用户的所有关联角色名和权限名
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowUserRoleModule(string Id)
        {
            var list=await userBLL.GetUserRoleModuleAsync(Id);
            return View(list);
        }

        /// <summary>
        /// 进入用户登录页
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
        /// <param name="Name"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> Login(UserDTO userDTO)
        {
            var user = await userBLL.LoginAsync(userDTO.Name, userDTO.Password);
            //数据库中有此人
            if (user != null)
            {
                Session["UserName"] = user.Name;
                Session["UserId"] = user.Id;
                return JsonConvert.SerializeObject(new ResultResponse() { State = "success" });
            }
            else
            {
                return JsonConvert.SerializeObject(new ResultResponse() { State = "fail", Message = "没有此人" });
            }
        }
    }
}