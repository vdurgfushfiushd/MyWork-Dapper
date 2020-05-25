using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ChatUserGroup
    {
        public string ChatUser_Id { get; set; }
        public ChatUser ChatUser { get; set; }
        public string Group_Id { get; set; }
        public Group Group { get; set; }
    }
}
