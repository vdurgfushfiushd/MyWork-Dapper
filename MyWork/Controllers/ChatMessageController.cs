using DTO;
using IBLL;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MyWork.Controllers
{
    public class ChatMessageController : Controller
    {
        public IUserBLL userBLL { get; set; }

        public IRoleBLL roleBLL { get; set; }

        public IModuleBLL moduleBLL { get; set; }

        public INoteBLL noteBLL { get; set; }

        public IChatMessageBLL chatMessageBLL { get; set; }

        public IChatUserBLL chatUserBLL { get; set; }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            //发送消息的人
            string CurrentChatName = Session["ChatName"].ToString();
            //在线的人
            var onlineNamelist=await chatUserBLL.GetFilterAsync(e=>e.IsOnline==true);
            return View(new { onlineNamelist, CurrentChatName });
        }

        /// <summary>
        /// 消息删除
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Delete()
        {
            return View();
        }

        /// <summary>
        /// 单个消息新增
        /// </summary>
        /// <param name="chatMessageDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> Add(ChatMessageDTO chatMessageDTO)
        {
            try
            {
                if (Session["ChatUserName"] == null)
                {
                    return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "fail",Message="请先进行聊天用户登录" });
                }
                else
                {
                    chatMessageDTO.Sender = Session["ChatUserName"].ToString();
                    await chatMessageBLL.AddAsync(chatMessageDTO);
                    return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "success" });
                }
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "fail", Message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<ActionResult> Update(string Id)
        {
            var entity=await chatMessageBLL.GetEntityAsync(new ChatMessageDTO() { Sender = "刘备", Receiver = "张飞",Id =Id});
            return View(entity);
        }

        [HttpPost]
        public async Task<ActionResult> Update(ChatMessageDTO chatMessageDTO)
        {
            await chatMessageBLL.GetEntityAsync(chatMessageDTO);
            return View();
        }
    }
}