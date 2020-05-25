using DTO;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public interface IGroupBLL
    {
        /// <summary>
        /// 群组新增
        /// </summary>
        /// <param name="groupDTO"></param>
        /// <returns></returns>
        Task AddAsync(GroupDTO groupDTO);

        /// <summary>
        /// 单个群组删除(软删除)
        /// </summary>
        /// <param name="groupDTO"></param>
        /// <returns></returns>
        Task DeleteAsync(GroupDTO groupDTO);

        /// <summary>
        /// 单个群组删除(真实删除)
        /// </summary>
        /// <param name="groupDTO"></param>
        /// <returns></returns>
        Task Delete_TrueAsync(GroupDTO groupDTO);

        /// <summary>
        /// 群组单个修改
        /// </summary>
        /// <param name="groupDTO"></param>
        /// <returns></returns>
        Task Update(GroupDTO groupDTO);

        /// <summary>
        /// 根据群组id获取指定群组的聊天用户集合,聊天信息集合
        /// </summary>
        /// <param name="GroupId"></param>
        /// <param name="GroupId"></param>
        /// <returns></returns>
        Task<GroupChatUserChatMessageDTO> GetGroupChatUserAsync(string GroupId);

        /// <summary>
        /// 获取所有isdeleted标志为false的group
        /// </summary>
        /// <returns></returns>
        Task<List<GroupDTO>> GetAllGroupDTOsAsync();

        /// <summary>
        /// 根据动态条件查询单个GroupDTO
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        Task<GroupDTO> GetEntityAsync(Dictionary<string, object> dict);

        /// <summary>
        /// 根据动态条件查询GroupDTO集合
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        Task<List<GroupDTO>> GetFilterAsync(Dictionary<string, object> dict);

        /// <summary>
        /// 根据动态条件删除指定的group
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        Task DeleteAsync(Dictionary<string, object> dict);
    }
}
