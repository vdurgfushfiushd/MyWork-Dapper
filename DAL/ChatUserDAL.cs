using IDAL;
using Model;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL
{
    public class ChatUserDAL : DapperHelper<ChatUser>,IChatUserDAL
    {
        public Task CreateGroupAsync(Group group)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Group>> GetGroupAsync(string ChatName)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ChatUser>> GetOnlineAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Message>> GetSendMessageAsync(string Sender, string Receiver)
        {
            throw new NotImplementedException();
        }

        public Task SendMessageAsync(Message message)
        {
            throw new NotImplementedException();
        }
    }
}
