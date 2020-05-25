using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DAL;
using DTO;
using IBLL;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace WebFront
{
    [HubName("ChatUserHub")]
    public class ChatUserHub : Hub
    {
        public IChatMessageBLL chatMessageBLL { get; set; }

        public static Dictionary<string, string> dict = new Dictionary<string, string>();

        //获取当前的connectionId
        public void GetCurrentConnectionId()
        {
            var ConnectionId=Context.ConnectionId;
            Clients.Client(ConnectionId).OnGetCurrentConnectionId( );
        }

        //将用户名和connectionId绑定
        public void BindChatName(string ChatName)
        {
            dict[ChatName] = Context.ConnectionId;
            Clients.All.OnBindChatName();
        }

        //获取指定的发送者和接收者之间的信息
        public async Task GetChatMessage(string Sender, string Receiver)
        {
            var SenderConnectionId = dict[Sender];
            var list=await chatMessageBLL.GetFiltersAllAsync(new ChatMessageDTO() { Sender= Sender, Receiver= Receiver });
            Clients.Client(SenderConnectionId).OnGetChatMessage(list);
        }

        //向指定的用户发送消息
        public void SendMessage(string Sender, string Receiver, string SendMessage)
        {
            var msg = Sender + " " + DateTime.Now + ":" + SendMessage;
            if (dict.ContainsKey(Sender))
            {
                var SenderConnectionId = dict[Sender];
                //给发送者自己发送，把用户的name传给自己
                Clients.Client(SenderConnectionId).OnSendMessage(msg);
            }
            if (dict.ContainsKey(Receiver))
            {
                var ReceiverConnectionId = dict[Receiver];
                //给接收者发送，把自己的name传给自己
                Clients.Client(ReceiverConnectionId).OnSendMessage(msg);
            }
        }
    }
} 