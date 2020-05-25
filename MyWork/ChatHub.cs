using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;
using IBLL;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Newtonsoft.Json;

namespace WebFront
{
    [HubName("ChatHub")]
    public class ChatHub : Hub
    {
        //获取在线用户
        public void Line()
        {
            var chatUserDAL = new ChatUserDAL();
            //var list = chatUserDAL.GetOnline();
            //Clients.All.OnLine(list);
        }
    }
}