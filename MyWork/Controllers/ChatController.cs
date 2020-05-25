using DTO;
using IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MyWork.Controllers
{
    public class ChatController : Controller
    {

        public IChatBLL chatBLL { get; set; }

        public IChatUserBLL chatUserBLL { get; set; }

        public IGroupBLL groupBLL { get; set; }  

        public IChatMessageBLL chatMessageBLL { get; set; }

        /// <summary>
        /// 聊天首页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            //聊天登录用户的id
            var chatUserId = Session["ChatUserId"].ToString();
            var chatIndexViewDTO=await chatBLL.GetChatIndexViewDTOAsync(new ChatUserDTO() {Id= chatUserId });
            return View(chatIndexViewDTO);
        }

        /// <summary>
        /// 聊天页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Chat(string ReceiverName)
        {
            ViewBag.ReceiverName = ReceiverName;
            //聊天登录用户的名字
            var sender = Session["ChatUserName"].ToString();
            ViewBag.SenderName = sender;
            var chatmessageDTOs = (await chatMessageBLL.GetFiltersAllAsync(new ChatMessageDTO() { Sender = sender, Receiver = ReceiverName })).OrderBy(e=>e.CreateTime);
            return View(chatmessageDTOs);
        }

        /// <summary>
        /// 群组聊天页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> GroupChat(string Id)
        {
            //聊天登录用户的名字
            ViewBag.SenderName = Session["ChatUserName"].ToString();
            var groupChatUserChatMessageDTO = (await groupBLL.GetGroupChatUserAsync(Id));
            return View(groupChatUserChatMessageDTO);
        }

        [HttpGet]
        public async Task<ActionResult> Exit(string CurrentChatName)
        {
            await chatUserBLL.ExitAsync(e=>e.Name==CurrentChatName);
            return View();
        }

    }
}