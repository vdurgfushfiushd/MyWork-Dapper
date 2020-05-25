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
    /// <summary>
    /// 音乐对象的业务逻辑层类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMusicBLL
    {
        /// <summary>
        /// 单个音乐新增
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Task AddAsync(MusicDTO t);

        /// <summary>
        /// 单个音乐删除(软删除)
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Task DeleteAsync(MusicDTO musicDTO);

        /// <summary>
        /// 单个对象删除(真实删除)
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        Task Delete_TrueAsync(MusicDTO musicDTO);

        /// <summary>
        /// 单个修改音乐
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Task UpdateAsync(MusicDTO t);

        /// <summary>
        /// 获取所有isdeleted标志为false的music
        /// </summary>
        /// <returns></returns>
        Task<List<MusicDTO>> GetAllMusicDTOsAsync();

        /// <summary>
        /// 根据动态条件查询单个MusicDTO
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        Task<MusicDTO> GetEntityAsync(Dictionary<string, object> dict);

        /// <summary>
        /// 根据动态条件查询MusicDTO集合
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        Task<List<MusicDTO>> GetFilterAsync(Dictionary<string, object> dict);

        /// <summary>
        /// 根据动态条件删除指定的music
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        Task DeleteAsync(Dictionary<string, object> dict);
    }
}
