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
    public class SingerBLL: ISingerBLL
    {
        public ISingerDAL singerDAL { get; set; }

        public IMusicDAL musicDAL { get; set; }

        /// <summary>
        /// 单个歌手新增
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task AddAsync(SingerDTO singerDTO)
        {
            try
            {
                //数据库中对应的数据
                string sql1 = @"select * from t_singers where Id=@Id and IsDeleted=false";
                var db_singer = await singerDAL.GetEntityAsync(sql1, singerDTO);
                //数据库中没有，则新增
                if (db_singer == null)
                {
                    //要新增的数据
                    var singer = Mapper.Map<Singer>(singerDTO);
                    //属性修改
                    singer.Id = Guid.NewGuid().ToString("n");
                    singer.IsDeleted = false;
                    singer.CreateTime = DateTime.Now;
                    singer.FirstLetter = singerDTO.FirstLetter;
                    singer.Sex = singerDTO.Sex;
                    singer.Picture = singerDTO.Picture;
                    //数据新增
                    string sql2 = @"insert into t_singers(Id,Name,FirstLetter,Style,Country,Sex,Picture,IsDeleted,CreateTime) values(@Id,@Name,@FirstLetter,@Style,@Country,@Sex,@Picture,@IsDeleted,@CreateTime)";
                    await singerDAL.ExecuteAsync(sql2, singer);
                }
                singerDAL.Commit();
            }
            catch (Exception)
            {
                singerDAL.Rollback();
            }
        }

        /// <summary>
        /// 单个对象删除(软删除)
        /// </summary>
        /// <param name="singerDTO"></param>
        /// <returns></returns>
        public async Task DeleteAsync(SingerDTO singerDTO)
        {
            try
            {
                //数据库中对应的数据
                string sql1 = @"select * from t_singers where Id=@Id and IsDeleted=false";
                var db_singer = await singerDAL.GetEntityAsync(sql1, singerDTO);
                //数据库中有，则删除
                if (db_singer != null)
                {
                    string sql2 = @"update t_singers set IsDeleted=true where Id=@Id";
                    await singerDAL.ExecuteAsync(sql2, db_singer);
                }
                singerDAL.Commit();
            }
            catch (Exception)
            {
                singerDAL.Rollback();
            }
        }

        /// <summary>
        /// 单个对象删除(真实删除)
        /// </summary>
        /// <param name="singerDTO"></param>
        /// <returns></returns>
        public async Task Delete_TrueAsync(SingerDTO singerDTO)
        {
            try
            {
                //数据库中对应的数据
                string sql1 = @"select * from t_singers where Id=@Id and IsDeleted=false";
                var db_singer = await singerDAL.GetEntityAsync(sql1, singerDTO);
                //数据库中有，则删除
                if (db_singer != null)
                {
                    //删除歌手数据表中的对应数据
                    string sql2 = @"delete from t_singers where Id=@Id";
                    var singer = Mapper.Map<Singer>(db_singer);
                    await singerDAL.ExecuteAsync(sql2, singer);
                }
                singerDAL.Commit();
            }
            catch (Exception)
            {
                singerDAL.Rollback();
            }
        }

        /// <summary>
        /// 获取所有isdeleted为false的歌手
        /// </summary>
        /// <returns></returns>
        public async Task<List<SingerDTO>> GetAllSingerDTOsAsync()
        {
            string sql = @"select * from t_singers where IsDeleted=false";
            var result= (await singerDAL.GetFilterAsync(sql, null)).ToList();
            return result.Select(e => Mapper.Map<SingerDTO>(e)).ToList();
        }

        /// <summary>
        /// 单纯的修改歌手信息
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task UpdateAsync(SingerDTO singerDTO)
        {
            try
            {
                //数据库中对应的数据
                string sql1 = @"select * from t_singers where Id=@Id and IsDeleted=false";
                var db_singer = await singerDAL.GetEntityAsync(sql1, singerDTO);
                //修改歌手的属性
                if (db_singer != null)
                {
                    db_singer.Name = singerDTO.Name;
                    db_singer.Sex = singerDTO.Sex;
                    db_singer.Style = singerDTO.Style;
                    db_singer.Country = singerDTO.Country;
                    string sql2 = @"update t_singers set Name=@Name,Sex=@Sex,Style=@Style,Country=@Country where Id=@Id";
                    await singerDAL.ExecuteAsync(sql2, db_singer);
                }
                singerDAL.Commit();
            }
            catch (Exception)
            {
                singerDAL.Rollback();
            }
        }

        /// <summary>
        /// 根据动态条件查询单个singer
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        public async Task<SingerDTO> GetEntityAsync(Dictionary<string, object> dict)
        {
            string sql = "select * from t_singers where 1=1";
            var result = (await singerDAL.ByWhere(sql, dict)).FirstOrDefault();
            if (result != null)
                return Mapper.Map<SingerDTO>(result);
            return null;
        }

        /// <summary>
        /// 根据动态条件查询singer集合
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        public async Task<List<SingerDTO>> GetFilterAsync(Dictionary<string, object> dict)
        {
            string sql = "select * from t_singers where 1=1";
            var result = (await singerDAL.ByWhere(sql, dict)).ToList();
            return result.Select(e => Mapper.Map<SingerDTO>(e)).ToList();
        }

        /// <summary>
        /// 根据动态条件删除指定的singer
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Dictionary<string, object> dict)
        {
            string sql = "delete from t_singers where 1=1";
            await singerDAL.ExecuteAsync(sql, dict);
        }
    }
}
