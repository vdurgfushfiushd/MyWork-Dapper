
using DTO;
using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IBLL
{
    /// <summary>
    /// 日志对象的业务逻辑层类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface INoteBLL
    {
        /// <summary>
        /// 将指定日期的日志转换为MemoryStream
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        MemoryStream ExportToExcelAsync(DateTime startTime, DateTime endTime);

        /// <summary>
        /// 根据起止日期查询日志
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        Task<List<NoteDTO>> GetNotesByDateAsync(DateTime startTime, DateTime endTime);

        /// <summary>
        /// 单个日志新增
        /// </summary>
        /// <param name="noteDTO"></param>
        /// <returns></returns>
        Task AddAsync(NoteDTO noteDTO);

        /// <summary>
        /// 单个日志删除(软删除)
        /// </summary>
        /// <param name="noteDTO"></param>
        /// <returns></returns>
        Task DeleteAsync(NoteDTO noteDTO);

        /// <summary>
        /// 单个日志删除
        /// </summary>
        /// <param name="noteDTO"></param>
        /// <returns></returns>
        Task Delete_TrueAsync(NoteDTO noteDTO);

        /// <summary>
        /// 日志修改
        /// </summary>
        /// <param name="noteDTO"></param>
        /// <returns></returns>
        Task UpdateAsync(NoteDTO noteDTO);

        /// <summary>
        /// 获取所有isdeleted标志为false的note
        /// </summary>
        /// <returns></returns>
        Task<List<NoteDTO>> GetAllNoteDTOsAsync();

        /// <summary>
        /// 根据动态条件查询单个NoteDTO
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        Task<NoteDTO> GetEntityAsync(Dictionary<string, object> dict);

        /// <summary>
        /// 根据动态条件查询RNoteDTO集合
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        Task<List<NoteDTO>> GetFilterAsync(Dictionary<string, object> dict);

        /// <summary>
        /// 根据动态条件删除指定的note
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        Task DeleteAsync(Dictionary<string, object> dict);
    }
}
