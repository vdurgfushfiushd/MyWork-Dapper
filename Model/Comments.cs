
using MongoDB.Bson;
using System;

namespace Model
{
    /// <summary>
    /// 评论类
    /// </summary>
    public class Comments : IEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        public ObjectId Id { get; set; }
        /// <summary>
        /// 评论人
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 评论人ID
        /// </summary>
        public long User_Id { get; set; }
        /// <summary>
        /// 评论歌曲
        /// </summary>
        public string MusicName { get; set; }
        /// <summary>
        /// 评论内容
        /// </summary>
        public string CommentDetail { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 评论时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 评论和歌曲的一对多关系
        /// </summary>
        public virtual Music Music { get; set; }
        /// <summary>
        /// 歌曲ID
        /// </summary>
        public long Music_Id { get; set; }
       
    }
}
