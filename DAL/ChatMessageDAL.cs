using IDAL;
using Model;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ChatMessageDAL : IChatMessageDAL
    {
        //连接字符串
        public static string connstr = ConfigurationManager.ConnectionStrings["redisconnstr"].ConnectionString;

        //redis连接对象
        public static ConnectionMultiplexer conn = ConnectionMultiplexer.Connect(connstr);

        //连接的数据库
        public static IDatabase db = conn.GetDatabase(0);

        public IGroupDAL groupDAL { get; set; }

        /// <summary>
        /// 新增单个chatmessage(一对一发送消息)
        /// </summary>
        /// <param name="chatMessage"></param>
        /// <returns></returns>
        public async Task AddAsync(ChatMessage chatMessage)
        {
            var jsonstring = JsonConvert.SerializeObject(chatMessage);
            await db.ListLeftPushAsync("ChatMessage-" + chatMessage.Sender + "To" + chatMessage.Receiver, jsonstring);
        }

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="chatMessages"></param>
        /// <returns></returns>
        public async Task AddRangeAsync(List<ChatMessage> chatMessages)
        {
            foreach (var chatMessage in chatMessages)
            {
                var jsonstring = JsonConvert.SerializeObject(chatMessage);
                await db.ListLeftPushAsync("ChatMessage-" + chatMessage.Sender + "To" + chatMessage.Receiver, jsonstring);
            }
        }

        /// <summary>
        /// 删除指定的chatmessage
        /// </summary>
        /// <param name="chatMessage"></param>
        /// <returns></returns>
        public async Task DeleteAsync(ChatMessage chatMessage)
        {
            var jsonstring = JsonConvert.SerializeObject(chatMessage);
            await db.ListRemoveAsync("ChatMessage-" + chatMessage.Sender + "To" + chatMessage.Receiver, jsonstring);
        }

        /// <summary>
        /// 获取单个chatmessage
        /// </summary>
        /// <param name="chatMessage"></param>
        /// <returns></returns>
        public async Task<ChatMessage> GetEntityAsync(ChatMessage chatMessage)
        {
            var list=await GetFiltersAsync(chatMessage);
            return list.Where(e => e.Id == chatMessage.Id).FirstOrDefault();
        }

        /// <summary>
        /// 查询指定发送者和接受者之间的所有聊天记录(单向信息)
        /// </summary>
        /// <param name="chatMessage"></param>
        /// <returns></returns>
        public async Task<List<ChatMessage>> GetFiltersAsync(ChatMessage chatMessage)
        {
            List<ChatMessage> list = new List<ChatMessage>();
            var redisValues = await db.ListRangeAsync("ChatMessage-" + chatMessage.Sender + "To" + chatMessage.Receiver);
            foreach (var redisValue in redisValues)
            {
                var entity = JsonConvert.DeserializeObject<ChatMessage>(redisValue);
                list.Add(entity);
            }
            return list;
        }

        /// <summary>
        /// 查询发送者和接受者之间的所有聊天记录(双向信息)
        /// </summary>
        /// <param name="chatMessage"></param>
        /// <returns></returns>
        public async Task<List<ChatMessage>> GetFiltersAllAsync(ChatMessage chatMessage)
        {
            List<ChatMessage> list = new List<ChatMessage>();
            var redisValues1 = (await db.ListRangeAsync("ChatMessage-" + chatMessage.Sender + "To" + chatMessage.Receiver)).ToList();
            var redisValues2 = (await db.ListRangeAsync("ChatMessage-" + chatMessage.Receiver + "To" + chatMessage.Sender)).ToList();
            redisValues1.AddRange(redisValues2);
            foreach (var redisValue in redisValues1)
            {
                var entity = JsonConvert.DeserializeObject<ChatMessage>(redisValue);
                list.Add(entity);
            }
            return list;
        }

        /// <summary>
        /// 查询指定发送者和接受者之间的指定日期的聊天记录
        /// </summary>
        /// <param name="chatMessage"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public async Task<List<ChatMessage>> GetFiltersAsync(ChatMessage chatMessage, DateTime startTime, DateTime endTime)
        {
            var list =await GetFiltersAsync(chatMessage);
            return list.Where(e => DateTime.Compare(e.CreateTime, startTime) >= 0 && DateTime.Compare(e.CreateTime, endTime) <= 0).ToList();
        }

        /// <summary>
        /// 模糊查询聊天消息集合
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public async Task<List<ChatMessage>> GetFiltersAsync(string keyword)
        {
            //默认一个服务器
            var server = conn.GetServer(conn.GetEndPoints()[0]);
            //通配符
            var pattern = "*" + keyword + "*";
            //匹配的键的集合
            var keys = server.Keys(pattern: pattern);
            //var keys = server.Keys(database: db.Database, pattern: pattern); //StackExchange.Redis 会根据redis版本决定用keys还是   scan(>2.8) 
            //var keys= (string[])db.ScriptEvaluate(LuaScript.Prepare("return redis.call('KEYS',@keypattern)"), new { keypattern = keyword });
            //返回的集合
            var list = new List<ChatMessage>();
            //匹配的键的集合
            foreach (var key in keys)
            {
                //每个键对应的值的集合
                var redisvalues = await db.ListRangeAsync(key);
                foreach (var redisvalue in redisvalues)
                {
                    var entity = JsonConvert.DeserializeObject<ChatMessage>(redisvalue);
                    list.Add(entity);
                }
            }
            return list;
        }

        /// <summary>
        /// 修改指定chatmessage的内容
        /// </summary>
        /// <param name="chatMessage"></param>
        /// <returns></returns>
        public async Task UpdateAsync(ChatMessage chatMessage)
        {
            //获取要修改的对象
            var chatmessage = await GetEntityAsync(chatMessage);
            //修改对象的属性
            chatmessage.Message = chatMessage.Message;
        }
    }
}
