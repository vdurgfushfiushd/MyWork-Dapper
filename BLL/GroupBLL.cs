using AutoMapper;
using DTO;
using IBLL;
using IDAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class GroupBLL : IGroupBLL
    {
        public IGroupDAL groupDAL { get; set; }

        public IChatUserDAL chatUserDAL { get; set; }

        public IChatMessageDAL chatMessageDAL { get; set; }

        /// <summary>
        /// 群组新增
        /// </summary>
        /// <param name="groupDTO"></param>
        /// <returns></returns>
        public async Task AddAsync(GroupDTO groupDTO)
        {
            //数据库中对应的数据
            string sql1 = @"select * from t_groups where Id=@Id";
            var db_group = await groupDAL.GetEntityAsync(sql1,groupDTO);
            if (db_group == null)
            {
                //要新增的数据
                var group = Mapper.Map<Group>(groupDTO);
                //设置属性
                group.Id = Guid.NewGuid().ToString("n");
                group.IsDeleted = false;
                group.CreateTime = DateTime.Now;
                //新增
                string sql2 = @"insert into t_groups(Id,GroupName,Describe,CreateTime,IsDeleted) values(@Id,@GroupName,@Describe,@CreateTime,@IsDeleted)";
                await groupDAL.ExecuteAsync(sql2,group);
            }
        }

        /// <summary>
        /// 群组单个删除(软删除)
        /// </summary>
        /// <param name="groupDTO"></param>
        /// <returns></returns>
        public async Task DeleteAsync(GroupDTO groupDTO)
        {
            //数据库中对应的数据
            string sql1 = @"update t_groups set IsDeleted=true where Id=@Id";
            var db_group = await groupDAL.GetEntityAsync(sql1,groupDTO);
        }

        /// <summary>
        /// 群组单个删除(真实删除)
        /// </summary>
        /// <param name="groupDTO"></param>
        /// <returns></returns>
        public async Task Delete_TrueAsync(GroupDTO groupDTO)
        {
            //删除群组表中对应的数据
            string sql1 = @"delete from t_groups where Id=@Id";
            await groupDAL.ExecuteAsync(sql1,groupDTO);
            //删除聊天用户和群组关系表的数据
            string sql2 = @"delete from t_chatusergrouprelations where Group_Id=@Id";
            await groupDAL.ExecuteAsync(sql2, groupDTO);
        }

        /// <summary>
        /// 群组修改
        /// </summary>
        /// <param name="groupDTO"></param>
        /// <returns></returns>
        public async Task Update(GroupDTO groupDTO)
        {
            //数据库中对应的数据
            string sql1 = @"select * from t_groups where Id=@Id and IsDeleted=false";
            var db_group = await groupDAL.GetEntityAsync(sql1,groupDTO);
            //修改群组的属性
            db_group.GroupName = groupDTO.GroupName;
            db_group.Describe = groupDTO.Describe;
            string sql2 = @"update t_groups set GroupName=#GroupName,Describe=@Describe where Id=@Id";
            await groupDAL.ExecuteAsync(sql2,db_group);
        }

        /// <summary>
        /// 根据群组id获取指定群组的聊天用户集合,聊天信息集合
        /// </summary>
        /// <param name="GroupId"></param>
        /// <param name="GroupId"></param>
        /// <returns></returns>
        public async Task<GroupChatUserChatMessageDTO> GetGroupChatUserAsync(string GroupId)
        {
            //数据库中对应的群组对象
            string sql1 = @"select * from t_groups where Id=@Id and IsDeleted=false";
            var db_group = await groupDAL.GetEntityAsync(sql1,new { Id=GroupId});
            //群组对应的聊天用户集合
            string sql2 = @"select * from t_groups as group join t_chatusergrouprelations as chatusergroup on group.Id=chatusergroup.Group_Id join t_chatusers as chatuser on chatusergroup.ChatUser_Id=chatuser.Id where group.Id=@Id and IsDeleted=false";
            var db_chatusers = await groupDAL.GetFilterAsync(sql2,new { Id= GroupId });
            //群组对应的聊天信息集合
            var db_chatmessages = (await chatMessageDAL.GetFiltersAsync(db_group.GroupName)).OrderBy(e=>e.CreateTime);
            //返回的对象
            return new GroupChatUserChatMessageDTO() { GroupDTO=Mapper.Map<GroupDTO>(db_group),ChatUserDTOs=db_chatusers.Select(e=>Mapper.Map<ChatUserDTO>(e)).ToList(), ChatMessageDTOs = db_chatmessages.Select(e=>Mapper.Map<ChatMessageDTO>(e)).ToList()};
        }

        /// <summary>
        /// 获取所有isdeleted标志为false的group
        /// </summary>
        /// <returns></returns>
        public async Task<List<GroupDTO>> GetAllGroupDTOsAsync()
        {
            string sql = @"select * from t_groups where IsDeleted=false";
            var result = (await groupDAL.GetFilterAsync(sql, null)).ToList();
            return result.Select(e => Mapper.Map<GroupDTO>(e)).ToList();
        }

        /// <summary>
        /// 根据动态条件查询单个module
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        public async Task<GroupDTO> GetEntityAsync(Dictionary<string, object> dict)
        {
            string sql = "select * from t_groups where 1=1";
            var result = (await groupDAL.ByWhere(sql, dict)).FirstOrDefault();
            if (result != null)
                return Mapper.Map<GroupDTO>(result);
            return null;
        }

        /// <summary>
        /// 根据动态条件查询module集合
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        public async Task<List<GroupDTO>> GetFilterAsync(Dictionary<string, object> dict)
        {
            string sql = "select * from t_groups where 1=1";
            var result = (await groupDAL.ByWhere(sql, dict)).ToList();
            return result.Select(e => Mapper.Map<GroupDTO>(e)).ToList();
        }

        /// <summary>
        /// 根据动态条件删除指定的group
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Dictionary<string, object> dict)
        {
            string sql = "delete from t_groups where 1=1";
            await groupDAL.ExecuteAsync(sql, dict);
        }

    }
}
