using AutoMapper;
using MyWork.AutoMapperProfile;

namespace MyWork.App_Start
{
    public class AutoMapperConfig
    {
        public static void Config()
        {
            Mapper.Initialize(cfg =>
            {
                //将要注册的配置文件注册
                cfg.AddProfile<UserMapperProfile>();
                cfg.AddProfile<RoleMapperProfile>();
                cfg.AddProfile<ModuleMapperProfile>();
                cfg.AddProfile<MusicMapperProfile>();
                cfg.AddProfile<NoteMapperProfile>();
                cfg.AddProfile<ChatMessageMapperProfile>();
                cfg.AddProfile<CommentsMapperProfile>();
                cfg.AddProfile<ChatUserMapperProfile>();
                cfg.AddProfile<SingerMapperProfile>();
                cfg.AddProfile<GroupMapperProfile>();

                //如果还有其他的实体配置文件，接着往下加就可以
            });
        }
    }
}