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
    /// 歌手对象的业务逻辑层类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISingerBLL
    {
        /// <summary>
        /// 单个歌手新增
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Task AddAsync(SingerDTO singerDTO);

        /// <summary>
        /// 单个对象删除(软删除)
        /// </summary>
        /// <param name="singerDTO"></param>
        /// <returns></returns>
        Task DeleteAsync(SingerDTO singerDTO);

        /// <summary>
        /// 单个对象删除(真实删除)
        /// </summary>
        /// <param name="singerDTO"></param>
        /// <returns></returns>
        Task Delete_TrueAsync(SingerDTO singerDTO);

        /// <summary>
        /// 单纯的修改歌手信息
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Task UpdateAsync(SingerDTO t);

        /// <summary>
        /// 获取所有isdeleted标志为false的singer
        /// </summary>
        /// <returns></returns>
        Task<List<SingerDTO>> GetAllSingerDTOsAsync();

        /// <summary>
        /// 根据动态条件查询单个SingerDTO
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        Task<SingerDTO> GetEntityAsync(Dictionary<string, object> dict);

        /// <summary>
        /// 根据动态条件查询SingerDTO集合
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        Task<List<SingerDTO>> GetFilterAsync(Dictionary<string, object> dict);

        /// <summary>
        /// 根据动态条件删除指定的singer
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        Task DeleteAsync(Dictionary<string, object> dict);
    }
}
