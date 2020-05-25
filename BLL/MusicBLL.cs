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
    public class MusicBLL: IMusicBLL
    {
        public IUserDAL userDAL { get; set; }

        public IRoleDAL roleDAL { get; set; }

        public IModuleDAL moduleDAL { get; set; }

        public INoteDAL noteDAL { get; set; }

        public ISingerDAL singerDAL { get; set; }

        public IMusicDAL musicDAL { get; set; }

        /// <summary>
        /// 单个音乐新增
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task AddAsync(MusicDTO musicDTO)
        {
            //数据库中的数据
            string sql1 = @"select * from t_musics where Id=@Id";
            var db_music = await musicDAL.GetEntityAsync(sql1,musicDTO);
            if (db_music == null)
            {
                //要新增的数据
                var music = Mapper.Map<Music>(db_music);
                //属性设置
                music.Id = Guid.NewGuid().ToString("n");
                music.IsDeleted = false;
                music.CreateTime = DateTime.Now;
                //音乐新增
                string sql2 = @"insert into t_notes(Id,Name,Language,Style,Theme,IsDeleted,CreateTime,Url) values(@Id,@Name,@Language,@Style,@Theme,@IsDeleted,@CreateTime,@Url)";
                await musicDAL.ExecuteAsync(sql2,music);
            }
        }

        /// <summary>
        /// 单个音乐删除(软删除)
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task DeleteAsync(MusicDTO musicDTO)
        {
            //数据库中对应的音乐数据
            string sql1 = @"select * from t_musics where Id=@Id";
            var db_music = await musicDAL.GetEntityAsync(sql1,musicDTO);
            if (db_music != null)
            {
                string sql2 = @"update t_musics set IsDeleted=true where Id=@Id";
                await musicDAL.ExecuteAsync(sql2, db_music);
            }
        }

        /// <summary>
        /// 单个音乐删除(真实删除)
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task Delete_TrueAsync(MusicDTO musicDTO)
        {
            //删除数据库中对应的音乐数据
            string sql1 = @"delete from t_musics where Id=@Id";
            var db_music = await musicDAL.GetEntityAsync(sql1,musicDTO);
        }

        /// <summary>
        /// 根据条件动态单个获取音乐
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public async Task<MusicDTO> GetEntityAsync(Expression<Func<Music, bool>> exp)
        {
            return null;
        }

        /// <summary>
        /// 单个修改音乐
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task UpdateAsync(MusicDTO musicDTO)
        {
            //数据库中的数据
            string sql1 = @"update t_musics set Name=@Name,Language=@Language,Style=@Style,Theme=@Theme,Url=@Url where Id=@Id";
            var db_music = await musicDAL.GetEntityAsync(sql1,musicDTO);
        }

        /// <summary>
        /// 获取所有isdeleted标志为false的music
        /// </summary>
        /// <returns></returns>
        public async Task<List<MusicDTO>> GetAllMusicDTOsAsync()
        {
            string sql = @"select * from t_musics where IsDeleted=false";
            var result = (await musicDAL.GetFilterAsync(sql, null)).ToList();
            return result.Select(e => Mapper.Map<MusicDTO>(e)).ToList();
        }

        /// <summary>
        /// 根据动态条件查询单个music
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        public async Task<MusicDTO> GetEntityAsync(Dictionary<string, object> dict)
        {
            string sql = "select * from t_musics where 1=1";
            var result = (await musicDAL.ByWhere(sql, dict)).FirstOrDefault();
            if (result != null)
                return Mapper.Map<MusicDTO>(result);
            return null;
        }

        /// <summary>
        /// 根据动态条件查询music集合
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        public async Task<List<MusicDTO>> GetFilterAsync(Dictionary<string, object> dict)
        {
            string sql = "select * from t_musics where 1=1";
            var result = (await musicDAL.ByWhere(sql, dict)).ToList();
            return result.Select(e => Mapper.Map<MusicDTO>(e)).ToList();
        }

        /// <summary>
        /// 根据动态条件删除指定的music
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Dictionary<string, object> dict)
        {
            string sql = "delete from t_musics where 1=1";
            await musicDAL.ExecuteAsync(sql, dict);
        }
    }
}
