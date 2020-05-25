using System.Collections.Generic;

namespace DTO
{
    /// <summary>
    /// 聊天用户登录
    /// </summary>
    public class ChatUserLoginDTO
    {
        public ChatUserDTO LoginChatUserDTO { get; set; }
        public List<ChatUserDTO> OnLineChatUserDTO { get; set; }
        public List<GroupDTO> BelongGroup { get; set; }
        public ChatUserLoginDTO(ChatUserDTO LoginChatUserDTO, List<ChatUserDTO>  OnLineChatUserDTO, List<GroupDTO> BelongGroup)
        {
            this.LoginChatUserDTO = LoginChatUserDTO;
            this.OnLineChatUserDTO = OnLineChatUserDTO;
            this.BelongGroup = BelongGroup;

        }
    }
}
