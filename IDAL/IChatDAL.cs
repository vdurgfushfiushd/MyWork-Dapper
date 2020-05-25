
using Model;

namespace IDAL
{
    public interface IChatDAL
    {
        void Add(ChatUser chatUser);
        ChatUser Login(ChatUser chatUser);
    }
}
