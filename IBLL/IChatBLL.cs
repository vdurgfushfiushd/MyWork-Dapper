
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public interface IChatBLL
    {
        void Add(ChatUserDTO chatUserDTO);
        void Login(ChatUserDTO chatUserDTO);

        /// <summary>
        /// 获取指定用户的联系人和所属群组
        /// </summary>
        /// <param name="chatUserDTO"></param>
        /// <returns></returns>
        Task<ChatIndexViewDTO> GetChatIndexViewDTOAsync(ChatUserDTO chatUserDTO);
    }
}
