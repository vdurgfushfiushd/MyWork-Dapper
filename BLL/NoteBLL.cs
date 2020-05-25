using AutoMapper;
using CommonHelper;
using DTO;
using IBLL;
using IDAL;
using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BLL
{
    public class NoteBLL:INoteBLL
    {
        public INoteDAL noteDAL { get; set; }

        public IUserDAL userDAL { get; set; }

        public IRoleDAL roleDAL { get; set; }

        public IModuleDAL moduleDAL { get; set; }

        /// <summary>
        /// 将指定日期的日志转换为MemoryStream
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public MemoryStream ExportToExcelAsync(DateTime startTime,DateTime endTime)
        {
            string sql1 = @"select * from t_notes where CreateTime>=@startTime and CreateTime<=@endTime and IsDeleted=false";
            var list=noteDAL.GetFilterAsync(sql1,new { startTime= startTime, endTime = endTime }).Result.ToList();
            return ExcelHelper.ExportToExcel(list);
        }

        /// <summary>
        /// 根据起止日期查询日志
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public async Task<List<NoteDTO>> GetNotesByDateAsync(DateTime startTime,DateTime endTime)
        {
            string sql1 = @"select * from t_notes where CreateTime>=@startTime and CreateTime<=@endTime and IsDeleted=false";
            var list= (await noteDAL.GetFilterAsync(sql1, new { startTime = startTime, endTime = endTime })).ToList();
            return list.Select(e=>Mapper.Map<NoteDTO>(e)).ToList();
        }

        /// <summary>
        /// 单个日志新增
        /// </summary>
        /// <param name="noteDTO"></param>
        /// <returns></returns>
        public async Task AddAsync(NoteDTO noteDTO)
        {
            //数据库中对应的日志
            string sql1 = @"select * from t_notes where Id=@Id and IsDeleted == false";
            var db_note = await noteDAL.GetEntityAsync(sql1,new { Id=noteDTO.Id});
            //如果数据库中没有改数据，则新增
            if (db_note == null)
            {
                //要新增的数据
                var note = Mapper.Map<Note>(noteDTO);
                //属性修改
                note.Id = Guid.NewGuid().ToString("n");
                note.Name = noteDTO.Name;
                note.Detail = noteDTO.Detail;
                note.CreateTime = DateTime.Now;
                note.IsDeleted = false;
                note.User_Id = noteDTO.User_Id;
                //数据新增
                string sql2 = @"insert into t_notes(Id,Name,Detail,CreateTime,IsDeleted,User_Id) values(@Id,@Name,@Detail,@CreateTime,@IsDeleted,@User_Id)";
                await noteDAL.ExecuteAsync(sql2,note);
            }
        }

        /// <summary>
        /// 单个日志删除(软删除)
        /// </summary>
        /// <param name="noteDTO"></param>
        /// <returns></returns>
        public async Task DeleteAsync(NoteDTO noteDTO)
        {
            //数据库中的数据
            string sql1 = @"select * from t_notes where Id=@Id and IsDeleted=false";
            var db_note = await noteDAL.GetEntityAsync(sql1,new { Id=noteDTO.Id});
            //如果数据库中有该数据，则删除
            if (db_note!=null)
            {
                string sql2 = @"update t_notes set IsDeleted=true where Id=@Id";
                await noteDAL.ExecuteAsync(sql2,db_note);
            }
        }

        /// <summary>
        /// 单个日志删除(只是删除)
        /// </summary>
        /// <param name="noteDTO"></param>
        /// <returns></returns>
        public async Task Delete_TrueAsync(NoteDTO noteDTO)
        {
            //数据库中的数据
            string sql1 = @"select * from t_notes where Id=@Id and IsDeleted=false";
            var db_note = await noteDAL.GetEntityAsync(sql1,new { Id=noteDTO.Id});
            //如果数据库中有该数据，则删除
            if (db_note != null)
            {
                string sql2 = @"delete from t_notes where Id=@Id";
                await noteDAL.ExecuteAsync(sql2,db_note);
            }
        }

        /// <summary>
        /// 单个日志修改
        /// </summary>
        /// <param name="noteDTO"></param>
        /// <returns></returns>
        public async Task UpdateAsync(NoteDTO noteDTO)
        {
            //数据库表中对应的数据
            string sql = @"update t_notes set Name=@Name,Detail=@Detail where Id=@Id";
            var db_note = await noteDAL.GetEntityAsync(sql,noteDTO);
        }

        /// <summary>
        /// 获取所有isdeleted标志为false的note
        /// </summary>
        /// <returns></returns>
        public async Task<List<NoteDTO>> GetAllNoteDTOsAsync()
        {
            string sql = @"select * from t_notes where IsDeleted=false";
            var result = (await noteDAL.GetFilterAsync(sql, null)).ToList();
            return result.Select(e => Mapper.Map<NoteDTO>(e)).ToList();
        }

        /// <summary>
        /// 根据动态条件查询单个note
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        public async Task<NoteDTO> GetEntityAsync(Dictionary<string, object> dict)
        {
            string sql = "select * from t_notes where 1=1";
            var result = (await noteDAL.ByWhere(sql, dict)).FirstOrDefault();
            if (result != null)
                return Mapper.Map<NoteDTO>(result);
            return null;
        }

        /// <summary>
        /// 根据动态条件查询note集合
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        public async Task<List<NoteDTO>> GetFilterAsync(Dictionary<string, object> dict)
        {
            string sql = "select * from t_notes where 1=1";
            var result = (await noteDAL.ByWhere(sql, dict)).ToList();
            return result.Select(e => Mapper.Map<NoteDTO>(e)).ToList();
        }

        /// <summary>
        /// 根据动态条件删除指定的note
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Dictionary<string, object> dict)
        {
            string sql = "delete from t_notes where 1=1";
            await noteDAL.ExecuteAsync(sql, dict);
        }
    }
}
