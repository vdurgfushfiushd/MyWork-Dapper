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
    public class ChatUserController : Controller
    {
        public IUserBLL userBLL { get; set; }

        public IRoleBLL roleBLL { get; set; }

        public IModuleBLL moduleBLL { get; set; }

        public INoteBLL noteBLL { get; set; }

        public IChatUserBLL chatUserBLL { get; set; }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetAll()
        {
            var list = (await chatUserBLL.GetFilterAsync(e => e.IsDeleted == false)).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> GetData(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var list = (await chatUserBLL.GetFilterAsync(e =>e.Name== name && e.IsDeleted == false)).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var list = (await chatUserBLL.GetFilterAsync(e => e.IsDeleted == false)).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            } 
        }

        /// <summary>
        /// 聊天用户新增页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// 聊天用户新增
        /// </summary>
        /// <param name="chatUserDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> Add(ChatUserDTO chatUserDTO)
        {
            try
            {
                await chatUserBLL.AddAsync(chatUserDTO);
                return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "success"});
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "fail", Message = ex.Message });
            }
        }

        /// <summary>
        /// 修改页面
        /// </summary>
        /// <param name="Id">要修改的chatuser的id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Update(string Id)
        {
            var ChatUserGroupDTO = await chatUserBLL.GetUpdateChatUserAsync(Id);
            return View(ChatUserGroupDTO);
        }

        /// <summary>
        /// 聊天用户修改
        /// </summary>
        /// <param name="chatUserDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> Update(ChatUserDTO chatUserDTO,string[] Group_Ids)
        {
            try
            {
                await chatUserBLL.UpdateAsync(chatUserDTO, Group_Ids);
                return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "success" });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "fail", Message = ex.Message });
            }
        }

        /// <summary>
        /// 聊天用户删除
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Delete(string Id)
        {
            await chatUserBLL.DeleteAsync(e=>e.Id==Id && e.IsDeleted == false);
            return View("/ChatUser/Index");
        }

        /// <summary>
        /// 登录页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 聊天用户登录
        /// </summary>
        /// <param name="chatUserDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> Login(ChatUserDTO chatUserDTO)
        {
            var chatUser = await chatUserBLL.ChatUserLogin(chatUserDTO);
            if (chatUser != null)
            {
                Session["ChatUserName"] = chatUser.Name;
                Session["ChatUserId"] = chatUser.Id;
                return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "success" });
            }
            else
            {
                return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "fail", Message = "请检查用户名或密码" });
            }
        }

        /// <summary>
        /// 聊天用户退出登录
        /// </summary>
        /// <param name="chatUserDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> Exit(ChatUserDTO chatUserDTO)
        {
            //要退出的用户
            var chatUser = await chatUserBLL.GetEntityAsync(e=>e.Id== chatUserDTO.Id&&e.IsDeleted==false);
            if (chatUser != null)
            {
                Session["ChatUserName"] = null;
                Session["ChatUserId"] = null;
                return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "success" });
            }
            else
            {
                return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "fail", Message = "请检查用户名或密码" });
            }
        }

        /// <summary>
        /// 加入群组页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult JoinGroup()
        {
            ViewBag.ChatUserId = Session["ChatUserId"].ToString();
            return View();
        }

        /// <summary>
        /// 加入群组
        /// </summary>
        /// <param name="chatUserDTO">聊天用户</param>
        /// <param name="groupDTO">群组</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> JoinGroup(string ChatUserId,string GroupName)
        {
            try
            {
                ChatUserDTO chatUserDTO = new ChatUserDTO() { Id = ChatUserId };
                GroupDTO groupDTO = new GroupDTO(){ GroupName = GroupName };
                await chatUserBLL.JoinGroupAsync(chatUserDTO, groupDTO);
                return JsonConvert.SerializeObject(new ResponseClassDTO() { State="success"});
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "fail",Message=ex.Message });
            }
        }

        /// <summary>
        /// 添加好友页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddFriend()
        {
            ViewBag.ChatUserId = Session["ChatUserId"].ToString();
            return View();
        }

        /// <summary>
        /// 添加好友
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> AddFriend(ChatUserFriendDTO chatUserFriendDTO)
        {
            try
            {
                await chatUserBLL.AddFriendAsync(chatUserFriendDTO);
                return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "success" });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "fail", Message = ex.Message });
            }
        }
    }
}